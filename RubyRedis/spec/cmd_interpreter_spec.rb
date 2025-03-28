require_relative "../lib/redis360"

# NOTE: I use semicolons here in the tests. The execute method on
# CmdInterpreter does not handle them. It only processes one command at a
# time.

RSpec.describe Redis360::CmdInterpreter do
  context "Strings" do
    [
      ["new str string ; set str abcdefghij ; get str", "abcdefghij"],
      ["new str string ; set str abcdefghij ; strlen str", 10],
      ["new str string ; set str abcdefghij ; getrange str 3 6", "defg"],
      # counter functions
      ["new counter string ; set counter 10 ; incr counter", 11],
      ["new counter string ; set counter 10 ; decr counter", 9]
    ].each do |commands, exp_val|
      describe "executing \"#{commands}\"" do
        it "should return #{exp_val.inspect}" do
          commands = commands.split(";")
          test_cmd = commands.pop
          intr = Redis360::CmdInterpreter.new
          commands.each { |cmd| intr.execute(cmd) }

          expect(intr.execute(test_cmd)).to eq exp_val
        end
      end
    end

    it "should raise an exception for an invalid command" do
      intr = Redis360::CmdInterpreter.new
      intr.execute("new str string")

      expect { intr.execute("crazy-command str") }.to raise_error(NoMethodError)
    end
  end

  context "Lists" do
    [
      ["new alist List ; LPUSH alist 1 ; LLEN alist", 1],
      ["new alist List ; LPUSH alist 1 ; lpush alist 2 3 ; LLEN alist", 3],
      ["new alist List ; LPUSH alist 1 2 3 ; lindex alist 1", "2"],
      ["new alist List ; LPUSH alist 1 2 3 ; lindex alist -1", "3"],
      ["new alist List ; LPUSH alist 1 2 3 ; lrange alist 0 1", ["1", "2"]]
    ].each do |commands, exp_val|
      describe "executing \"#{commands}\"" do
        it "should return #{exp_val.inspect}" do
          commands = commands.split(";")
          test_cmd = commands.pop
          intr = Redis360::CmdInterpreter.new
          commands.each { |cmd| intr.execute(cmd) }

          expect(intr.execute(test_cmd)).to eq exp_val
        end
      end
    end

    it "should raise an exception for an invalid command" do
      intr = Redis360::CmdInterpreter.new
      intr.execute("new str list")

      expect { intr.execute("crazy-command str") }.to raise_error(NoMethodError)
    end
  end

  context "Hashes" do
    [
      ["new brown hash ; HSET brown one 11 ; HGET brown one", "11"],
      ["new brown hash ; HSET brown one 11 ; Hset brown two twenty ; hlen brown", 2],
      ["new brown hash ; HSET brown one 11 ; Hset brown two twenty ; hkeys brown", ["one", "two"]],
      ["new brown hash ; HSET brown one 11 ; Hset brown two twenty ; hvals brown", ["11", "twenty"]]
    ].each do |commands, exp_val|
      describe "executing \"#{commands}\"" do
        it "should return #{exp_val.inspect}" do
          commands = commands.split(";")
          test_cmd = commands.pop
          intr = Redis360::CmdInterpreter.new
          commands.each { |cmd| intr.execute(cmd) }

          expect(intr.execute(test_cmd)).to eq exp_val
        end
      end
    end

    it "should raise an exception for an invalid command" do
      intr = Redis360::CmdInterpreter.new
      intr.execute("new hash Hash")

      expect { intr.execute("crazy-command hash") }.to raise_error(NoMethodError)
    end
  end
end
