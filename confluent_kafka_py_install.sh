curl -L https://github.com/edenhill/librdkafka/archive/v0.9.3.tar.gz | tar xzf -
librdkafka-0.9.3
./configure --prefix=/usr
make -j
sudo make install
pip install confluent_kafka
