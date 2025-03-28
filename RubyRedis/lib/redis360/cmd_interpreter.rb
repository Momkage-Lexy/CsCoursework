module Redis360
  class CmdInterpreter
    def initialize
      # Initialize hash, for key value search
      @input = {}
    end

    def execute(cmd_line)
      # execute a command and return a value
      # you need to look if the key exists
      # if it does, invoke the right method on it
      # otherwise, you need to implement the new command

      parts = cmd_line.split
      # Get command and convert to lower
      cmd = parts[0].downcase
      # Set Key
      key = parts[1]

      if cmd == "new"
        # Class of object
        cls = parts[2].capitalize
        # Create instance from Redis360
        @input[key] = Redis360.const_get(cls).new
        "Created new #{cls} with key '#{key}'"
      else
        # Get object from input
        obj = @input[key]
        # Error if no key
        raise "KeyNotFoundError: #{key} does not exist" unless obj

        # Init method
        method = cmd.downcase
        # Set method arguments
        args = parts [2..]
        # Call method on object
        obj.send(method, *args)
      end
    end
  end
end
