#!/usr/bin/env python
#
# Copyright 2016 Confluent Inc.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
# http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
#

#
# Example high-level Kafka 0.9 balanced Consumer
#
from confluent_kafka import Consumer, KafkaException, KafkaError
import time
import sys
import getopt
import json
from pprint import pformat


def stats_cb(stats_json_str):
    stats_json = json.loads(stats_json_str)
    print('\nKAFKA Stats: {}\n'.format(pformat(stats_json)))


def print_usage_and_exit(program_name):
    sys.stderr.write('Usage: %s [options..] <bootstrap-brokers> <group> <topic1> <topic2> ..\n' % program_name)
    options = '''
 Options:
  -T <intvl>   Enable client statistics at specified interval (ms)
'''
    sys.stderr.write(options)
    sys.exit(1)


if __name__ == '__main__':

    # Consumer configuration
    # See https://github.com/edenhill/librdkafka/blob/master/CONFIGURATION.md
    conf = {'bootstrap.servers': "192.168.0.85", 'group.id': "grp1", 'session.timeout.ms': 6000,
            'default.topic.config': {'auto.offset.reset': 'smallest'}}

    # Create Consumer instance
    c = Consumer(**conf)


    def print_assignment(consumer, partitions):
        print('Assignment:', partitions)


    # Subscribe to topics
    c.subscribe(['raw-records'], on_assign=print_assignment)

    # Read messages from Kafka, print to stdout
    i = 0
    start_time = time.time()
    try:
        while True:
            msg = c.poll(timeout=1.0)
            if msg is None:
                continue
            if msg.error():
                # Error or event
                if msg.error().code() == KafkaError._PARTITION_EOF:
                    # End of partition event
                    sys.stderr.write('%% %s [%d] reached end at offset %d\n' %
                                     (msg.topic(), msg.partition(), msg.offset()))
                elif msg.error():
                    # Error
                    raise KafkaException(msg.error())
            else:
                # Proper message
                i = i + 1
                if i % 100000 == 0:
                    print(str(i) + " records processed @ " + str((i / (time.time() - start_time))) + " records per second.")

    except KeyboardInterrupt:
        sys.stderr.write('%% Aborted by user\n')

    # Close down consumer to commit final offsets.
    c.close()