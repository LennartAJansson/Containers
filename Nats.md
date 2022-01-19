```
nats str add NOTIFICATIONS --subjects "NOTIFICATIONS.*" --ack --max-msgs=-1 --max-bytes=-1 --max-age=1y --storage file --retention limits --max-msg-size=-1 --discard old --dupe-window="0s" --replicas 1

nats str ls

nats str info NOTIFICATIONS

nats pub NOTIFICATIONS.inbound 'new message'

nats str info NOTIFICATIONS


nats con add NOTIFICATIONS MONITOR --ack none --target monitor.NOTIFICATIONS --deliver last --replay instant --filter '' --heartbeat=10s --flow-control

nats sub monitor.NOTIFICATIONS

nats pub NOTIFICATIONS.inbound 'another message'

nats pub NOTIFICATIONS.inbound 'yet another message'

nats str info NOTIFICATIONS


nats con add NOTIFICATIONS MONITOR2 --ack none --target monitor2.NOTIFICATIONS --deliver last --replay instant --filter '' --heartbeat=10s --flow-control


nats con add NOTIFICATIONS MONITOR3 --ack none --target monitor3.NOTIFICATIONS --deliver all --replay instant --filter '' --heartbeat=10s --flow-control

```

https://hub.docker.com/_/nats  
https://docs.nats.io/running-a-nats-service/introduction/running/nats_docker/jetstream_docker  
http://thinkmicroservices.com/blog/2021/jetstream/nats-jetstream.html  
