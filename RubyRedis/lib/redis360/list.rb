module Redis360
  class List
    def initialize
      @data = []
    end

    def set(data)
      # Set data value
      @data = data
    end

    def get
      @data
    end

    def lpush(*input)
      # push input to index 0
      @data.unshift(*input)
      @data
    end

    def llen
      @data.length
    end

    def lindex(index)
      # Convert to integer
      index = index.to_i
      # Adjust count for negative index
      index += @data.length if index < 0
      # Return index value
      @data[index]
    end

    def lrange(start, stop)
      # Convert range to integer
      start = start.to_i
      stop = stop.to_i
      # Adjust count for negative index
      stop += @data.length if stop < 0
      # Return values of range
      @data[start..stop]
    end
  end
end
