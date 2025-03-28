# Redis Emulator
For this lab, you will implement a small subset of the commands implemented
by [Redis](https://redis.io/docs/latest/commands).

## Command Syntax
Commands take the form of: `cmd key args`

where:
 - `cmd` is a Redis command. It is case-insensitive. Different object types
   support different commands.
 - `key` is the name of the object we're acting on. Key names are
   case-sensitive.
 - `args` is zero or more arguments to `cmd`. Each command takes its own set
   of arguments.

Everything is separated by spaces. We are not supporting any quoting around
string arguments, so you don't have to recognized `hello world` as a single
string.

In "real" Redis, objects spring into existence the first time you execute a
command on the key. We're gonna cheat by introducing a command for creating
objects: `new key class`

where:
 - `new` is our "cheat" command to recognize creating a new object.
 - `key` is the name of the new object.
 - `class` is the case-insensitive name of the class of the object. See
   below.

## Interpreter
You will start out with a nearly-empty class for the command interpreter
called `CmdInterpreter`. It implements only one method, `execute`, that
takes a string containing a command, executes it, and returns any results.
Results might be strings or arrays - Ruby doesn't care (but the tests do).

The big rub is that you cannot dispatch commands with a giant `case`
statement or `if-else` chain. Instead, you will look up the object by its key and invoke
the command on the object directly.
If the object doesn't support that command (e.g., a command for hashes
applied to a string), throw a `NoMethodError` exception. Depending on how
you implement things, you might/should get that for free.
All that being said, I will tolerate a `case` statement for the `new`
command, although it would be cool if you could do it without that.

## Types to Implement

We will only implement three classes/object types:

 - String
 - List
 - Hash

These are the same names of the types in the Redis documentation. Beware,
they are also the names of built-in types in Ruby. To prevent a name clash,
our classes will reside in a module called `Redis360`.

We will only implement a subset of the functions/methods on the various
types - see the demonstration below. In "real Redis" some commands take a
bunch of "crazy" options (used for fancy applications) - we aren't
implementing those.

## Demonstration
Below is a demonstration of my implementation of the command interpreter and
the object classes. These are the only commands you have to implement.

```
int = Redis360::CmdInterpreter.new

int.execute("new str string")   # create a new String called str
int.execute("set str abcdefghijklmnop") # set str to abcdefghijklmnop
int.execute("get str")          # -> abcdefghijklmnop
int.execute("strlen str")       # -> 16
int.execute("getrange str 3 6") # -> "defg"

int.execute("new list List")    # create a new List called list
int.execute("lpush list 1")     # adds "1" to the head of the list -> ["1"]
int.execute("lpush list 2 3")   # adds "2" and "3" -> ["2", "3", "1"]
int.execute("llen list")        # -> 3
int.execute("lindex list 1")    # -> 3
int.execute("lindex list -1")   # -> 1
int.execute("lrange list 0 1")  # -> ["2", "3"]

int.execute("new counter String")   # create a new String called counter
int.execute("set counter 10")   # -> "10"
int.execute("incr counter")     # -> "11"
int.execute("get counter")      # -> "11"
int.execute("decr counter")     # -> "10"
int.execute("decr counter")     # -> "9"
int.execute("get counter")      # -> "9"

int.execute("NEW hash Hash")    # create a new Hash called hash
int.execute("HSET hash one 11") # set the key "one" to 11 -> "11"
int.execute("hget hash one")    # -> "11"
int.execute("HSET hash two 22") # -> "22"
int.execute("Hlen hash")        # -> "2"
int.execute("HKEYS hash")       # -> ["one", "two"]
int.execute("HVALS hash")       # -> ["11", "22"]
```

You're welcome to consult the Redis documentation for some of these
commands, but I've simplified them, and that demo is probably a better
source of information. Or, email me.

# Ruby Environment Setup

I used Ruby version 3.3.5 to develop the lab. Older versions should work, up
to a point. (The version of Ruby a project uses can often be found in a file
called `.ruby-version` in the root of the project.)
GitHub is running version Ruby 3.0.2, which works, too. I'd guess anything
better than 3.0 should work.

There are a few different options for setting up a Ruby environment
depending on what platform you're on. You can start with the
[official download page](https://www.ruby-lang.org/en/downloads/).

## Windows
For Windows, it looks like [RubyInstaller](https://rubyinstaller.org) is the
way to go.

## MacOS
For Mac, the current version that ships with MacOS is 2.6.x. In theory,
that might work for this project (there isn't anything fancy or new in
our code), but I'm not sure the tests would work.

## Linux/WSL
The OS package manager you used to install the C compiler should also be
able to install a version of Ruby.

There are a couple of third-party tools for installing and managing multiple
versions of Ruby on your computer. I recommend
[rbenv](https://github.com/rbenv/rbenv) over the other big one called
[RVM](http://rvm.io). (It's really annoying that when you pronounce them,
they sound so similar.) They both require the Xcode command line tools,
which you should already have.

After you install `rbenv` installing Ruby is easy: `rbenv install 3.3.5`.

## All Platforms

Use `ruby --version` to confirm that you're running the right version of Ruby.

To run tests, you need to install a couple of gems. Luckily, it's just one
command: `bundle install`.  That should install all the gems we need in the
`vendor` directory in the root of the repo.

## Testing
There is a functionality test based on the demo above.
The code for the tests is in the `spec` directory.
You can run it via:
`bundle exec rspec --format documentation`. That is the more verbose
"documentation" mode of the testing tool, [RSpec](http://rspec.info). A less
verbose version is: `bundle exec rspec`.

The other "test" we have is a code formatter called
[Standard](https://github.com/standardrb/standard). You invoke it as:
`bundle exec standardrb`.
It will complain if you're not following proper Ruby code formatting. If
you're using VS Code, there is an extension called Standard Ruby that will
do the formatting for you every time you save a file. (There's a setting you
have to enable for that.)

Both of these tests will run every time you push your code to GitHub. Your
objective is to get all of the tests to pass.

(Note for the curious, we run the commands in our project that use the gems
we installed by prefixing the commands with `bundle exec`. This sets up the
Ruby environment with all of our gems that are local to our project in
the `vendor` directory.)

```
**What were your thoughts about using Ruby?**
Ruby took some time to get the hand of but it's pretty similar to python as far as the structure and class methods. 
I really wanted to use semi colons because my brain is in c# right now, but other than that it took me a few days of studying and 
practicing to feel confident. I still had to google how to do a specific thing like converting to string and integer
but I have to do that with other languages anyways because I need refreshers from switching to different languages sometimes. 

**If you used ChatGPT last time, how was it to not use it this time? (You didn't did you.)**
It was fine, the only think that I appreciate about ChatGPT is that it saves me time. 
I spend sooo much time trying to find the correct answer to things and as a busy college student
having that immediate answer is very beneficial. I feel confident in my knowledge and my ability
to learn that I don't think ChatGPT plays a factor in dulling my learning experience. 

I still had to google and search for answers to things I didn't know about Ruby and found help on reddit, 
stack overflow and ruby docs. So it did not change the experience so much as it delayed my progress. 

**How hard/easy was this assignment? Did anything trip you up?**
Creating the methods for the object types was very easy. I've had a lot of practice in 
python doing something similar. Creating the interpreter took time. I'd say the thing that tripped me up
in the interpreter was trying to not over complicate the method when I'm still new to the language. 
It took breaking it down to -command breakdown, -get key, -call method, to where I really got what I needed to do. 

