# Getting start with Redis
## Set up Redis
Start redis server
```
docker run -p 6379:6379 --rm redis redis-server
```

Run Redis CLI
```
docker run -it --rm redis redis-cli -h host.docker.internal
```
### Working with String
```
set [Key] [Value]
get [key]
del [key]
keys *
keys c*
rename [key] [newkey]
renamex [key] [newkey] # check duplicate key
incr [key]
incrby [key]
incrbyfloat [key] [value]
decr [key]
decrby [key]
mset [key] [value] [key] [value] ...
mget [key] [key] ...
exists [key]
expire [key] [ttl]
```

### Working with List
```
rpush [key] [element] ...
lpush [key] [element] ...
rpop [key] [count]
lpop [key] [count]
lrange [key] [start] [stop]
```

### Working with Hash
```
hset [key] [field] [value]
hmset [key] [field] [value] [field] [value] ...
hget [key] [field]
hmget [key] [field] [field] ...
hgetall [key]
hincrby [key] [field] [increment]
hdel [key] [field] ...
hkeys [key]
hvals [key]
hexists [key] [field]
```
### Working with Set
```
sadd [key] [member] ...
smembers [key]
spop [key] [count]
scard [key] # number of elements
sismember [key] [member]
smismember [key] [member] ...
sunion [key1] [key2]
sunionstore [detination] [key1] [key2]
sinter [key1] [key2]
sinterstore [destination] [key1] [key1]
sdiff [key1] [key2]
sdiffstore [destination] [key1] [key2]
```
### Working with Sorted Set
```
zadd [key] [score] [member] [score member ...]
zrange [key] [start] [stop]
zrange [key] [start] [stop] [score]
zrangebyscore [key] [start] [stop]
zrevrange [key] [start] [stop]
zrevrange [key] [start] [stop] [score]
zrevrangebyscore [key] [start] [stop]
zrank [key] [member]
```

### Publish and Subscribe
Start subscriber instance
```
docker run -it --rm redis redis-cli -h host.docker.internal
```

```
publish [chanel] [message]
subscribe [chanel]
```
