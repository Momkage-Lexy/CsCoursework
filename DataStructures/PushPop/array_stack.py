from typing import Any

from arrayadt import Array


class ArrayStack:
    """ Class ArrayStack - representing a fixed-size stack using a 1D Array
                Stipulations:
                1. Must use an Array object as the internal data structure from the Array assignment.
                2. Must adhere to the docstring requirements per method, including raising
                   raising appropriate exceptions where indicated.
                3. Must achieve a minimum of 92% code coverage through unit testing.
    """

    def __init__(self, max_size: int = 0) -> None:
        """ Constructor
            Usage:  stack = ArrayStack(10)
            @:param max_size the desired max size of the stack
            @:return none
        """
        self._stack = Array(size=max_size)
        self._size = 0

    @staticmethod
    def clone(array_stack_instance: 'ArrayStack') -> 'ArrayStack':
        """ Clone the stack
                Usage:  stack = ArrayStack.clone(instance)
                @:param instance an ArrayStack instance to deep copy data from.
                @:return a deep object copy of the stack
                @:raises TypeError if instance is provided and it is not an ArrayStack instance
        """
        if array_stack_instance is not None and not isinstance(array_stack_instance, ArrayStack):
                raise TypeError('Instance is not a ArrayStack')
        
        stack = ArrayStack(array_stack_instance.max_size)
        stack._stack = Array.clone(array_stack_instance._stack)
        stack._size = array_stack_instance._size

        return stack

    def push(self, item: Any) -> None:
        """ Push an item onto the stack
                Usage:   stack.push(item)
                @:param item to enqueue
                @:return none
                @:raises IndexError if the stack is full
        """
        if self.full:
            raise IndexError('Stack is full')

        self._stack[self._size] = item
        self._size += 1

    def pop(self) -> Any:
        """ Pop an item from the stack and return the item
                Usage:   item = stack.pop()
                @:return item that is popped
                @:raises IndexError if the stack is empty
        """
        if self.empty:
            raise IndexError('Stack is empty')

        self._size -= 1
        return self._stack[self._size]

    def clear(self) -> None:
        """ Clear the stack
                Usage: stack.clear():
                @:return none
        """
        self._size = 0

    @property
    def top(self) -> Any:
        """ Get the item at the top of the stack
                Usage:   item = stack.top
                @:return item that is at the top of the stack
                @:raises IndexError if the stack is empty
        """
        if self.empty:
            raise IndexError('Stack is empty')

        return self._stack[self._size - 1]
    
    @property
    def max_size(self) -> int:
        """ Get the max size of the stack
                Usage:   max_size = stack.max_size
                @:return max_size the max_size of the stack
        """
        return len(self._stack)

    @property
    def full(self) -> bool:
        """ Check whether the stack is full
                Usage:   full = stack.full
                @:return full boolean as to whether the stack is full
        """
        return self.max_size == self._size

    @property
    def empty(self) -> bool:
        """ Check whether the stack is empty
                Usage:   empty = stack.empty
                @:return empty boolean as to whether the stack is empty
        """
        return self._size == 0

    def __eq__(self, other) -> bool:
        """ Equality operator ==
            Usage: stack1 == stack2
            @:param other the instance to compare self to
            @:return true if the stacks are equal (deep check)
        """
        if type(other) != type(self):
            return False

        if len(self) != len(other):
            return False

        return self._stack == other._stack

    def __len__(self) -> int:
        """ len operator for getting length of the stack
            Usage: length = len(stack)
            @:return the length of the Stack (number of items on the stack)
        """
        return self._size

    def __str__(self) -> str:
        """ Return a string representation of the data and structure
                Usage: print(stack):
                @:return str the string representation of the data and structure
        """
        return f'ArrayStack size: {self._size}, Elements (top is last): {str(self._stack)}'
