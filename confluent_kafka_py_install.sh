Run the following commands to install:

curl -L https://github.com/edenhill/librdkafka/archive/v0.9.3.tar.gz | tar xzf -
cd librdkafka-0.9.3
./configure --prefix=/usr
make -j
sudo make install
sudo pip install confluent_kafka
