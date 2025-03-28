from typing import Any
from copy import deepcopy


class Array:
    """ Class Array - representing 1D data using a List
            Stipulations:
            1. Must use a sized Python list as the internal data structure
            2. Must adhere to the docstring requirements per method, including raising
               raising appropriate exceptions where indicated.
            3. Must achieve a minimum of 92% code coverage through unit testing.
    """

    def __init__(self, *items, size=0) -> None:
        """ Constructor
            Usages:  1. array = Array(size=10)
                     2. array = Array('A', 'B', 'C') 
                     3. array = Array(['A', 'B', 'C'])
                     4. array = Array(('A', 'B', 'C'))
            @:param *items a variable list of arguments that contain the items to be deep copied into the Array.
            @:param size the desired size of the Array (use 0 if providing initialization items)
                     Note: An Array can be initialized using a list or tuple, which must be flattened.
            @:return none
            @:raises TypeError if instance is provided and it is not an Array instance
        """
        if len(items) == 0:
            self._items = [None] * size
        else:
            list(Array._flatten_helper(items))

    @staticmethod
    def _flatten_helper(items):
        """ This is a private helper function that flattens items down to individual items.
            This is not part of the public methods that are provided to students.
        """
        #check if items is an instance of tuple or list
        if isinstance(items, tuple) or isinstance(items, list):
            for item in items:
                yield from Array._flatten_helper(item)
        else:
            yield items

    def __getitem__(self, index: int) -> Any:
        """ Bracket operator for getting an item
            Usage: val = array[0]
            @:param index the desired index
            @:return the item at the index
            @:raises IndexError if the index is out of bounds
        """
        if index >= len(self):
            raise IndexError(f'Array index {index} is out of range')
        
        return self._items[index]

    def __setitem__(self, index: int, item: Any) -> None:
        """ Bracket operator for setting an item
            Usage: array[index] = val
            @:param index the desired index to set
            @:param item the desired item to set at index
            @:raises IndexError if the index is out of bounds
            @:return none
        """
        if index >= len(self):
            raise IndexError(f'Array index {index} is out of range')

        self._items[index] = item

    def __len__(self) -> int:
        """ len operator for getting length of the array
            Usage: for i in range(len(array))
            @:return the length of the Array
        """
        return len(self._items)

    def resize(self, new_size: int) -> None:
        """ Resize an Array
            Usage: array.resize(5)
            @:param new_size the desired new size
            @:return none
        """
        #Ensure new size isn't negative
        if new_size < 0:
            raise ValueError('Size cannot be negative')
        #Get current size of the array
        current_size = len(self)
        #Truncate array to fit new size if smaller than current size
        if new_size < current_size:
            self._items = self._items[:new_size]
        #Extend array to fit new size if larger than current size
        else:
            self._items.extend([None] * (new_size - current_size))

    def __eq__(self, other: 'Array') -> bool:
        """ Equality operator ==
            Usage: are_equal = array1 == array2
            @:param other the instance to compare self to
            @:return true if the arrays are equal (deep check)
        """
        if type(other) != type(self):
            return False

        if len(self) != len(other):
            return False

        for i in range(len(self)):
            if self[i] != other[i]:
                return False

        return True
    
    def __ne__(self, other: 'Array') -> bool:
        """ Non-equality operator !=
            Usage: are_equal = array1 != array2
            @:param other the instance to compare self to
            @:return true if the arrays are not equal (deep check)
        """
        return not self == other

    def __iter__(self) -> Any:
        """ Iterator operator
            Usage: for item in array:
            @:return yields the item at index
        """
        for item in self._items:
            yield item

    def __delitem__(self, index: int) -> None:
        """ Delete an item in the array. Copies the array contents from index + 1 down
            to fill the gap caused by deleting the item and shrinks the array size down by one
            Usage: del array[0]
            @:param index the desired index to delete
            @:raises IndexError if the index is out of bounds
            @:return none
        """
        if index >= len(self):
            raise IndexError(f'Array index {index} is out of range')

        del self._items[index]

    def __contains__(self, item: Any) -> bool:
        """ Contains operator (in)
            Usage: if 3 in array:
            @:param item the desired item to check whether it's in the array
            @:return true if the array contains the item
        """
        return item in self._items

    def __str__(self) -> str:
        """ Return a string representation of the data and structure
            Usage: print(array):
            @:return str the string representation of the data and structure
        """
        return '[' + ', '.join([str(i) for i in self._items]) + ']'
