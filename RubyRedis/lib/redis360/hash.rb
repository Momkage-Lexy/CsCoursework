module Redis360
  class Hash
    def initialize
      @data = {}
    end

    def hset(key, value)
      @data[key] = value
      value
    end

    def hget(key)
      @data[key]
    end

    def hlen
      @data.length
    end

    def hkeys
      # Return list of keys
      @data.keys
    end

    def hvals
      # Return list of values
      @data.values
    end
  end
end
