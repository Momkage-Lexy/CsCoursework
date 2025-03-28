module Redis360
  class String
    def initialize
      @data = ""
    end

    def set(data)
      # Set string value
      @data = data
      data
    end

    def get
      @data
    end

    def strlen
      @data.length
    end

    def getrange(start, stop)
      @data[start.to_i..stop.to_i]
    end

    def incr
      # Increment as integer
      @data = (@data.to_i + 1)
    end

    def decr
      # Decrement as integer
      @data = (@data.to_i - 1)
    end
  end
end
