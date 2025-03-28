require_relative "lib/redis360"

int = Redis360::CmdInterpreter.new
puts int.execute("NEW hash Hash")
puts int.execute("HSET hash one 11")
puts int.execute("hget hash one")
puts int.execute("HSET hash two 22")
puts int.execute("Hlen hash")
puts int.execute("HKEYS hash")
puts int.execute("HVALS hash")

puts int.execute("new list List")
puts int.execute("lpush list 1")
puts int.execute("lpush list 2 3")
puts int.execute("llen list")
puts int.execute("lindex list 1")
puts int.execute("lindex list -1")
puts int.execute("lrange list 0 1")

puts int.execute("new str string")
puts int.execute("set  str abcdefghijklmnop")
puts int.execute("get str")
puts int.execute("strlen str")
puts int.execute("getrange str 3 6")

puts int.execute("new counter String")
puts int.execute("set counter 10")
puts int.execute("incr counter")
puts int.execute("get counter")
puts int.execute("decr counter")
puts int.execute("decr counter")
puts int.execute("get counter")
