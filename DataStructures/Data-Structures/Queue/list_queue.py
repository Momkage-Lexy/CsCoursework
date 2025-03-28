from typing import Any

from linked_list import LinkedList


class ListQueue:
    """ Class ListQueue - representing a queue based on a LinkedList
            Stipulations:
            1. Must use a LinkedList as the internal data structure from the LinkedList assignment.
            2. Must adhere to the docstring requirements per method, including raising
               raising appropriate exceptions where indicated.
            3. Must achieve a minimum of 92% code coverage through unit testing.
    """

    def __init__(self) -> None:
        """ Constructor
            Usage:  queue = ListQueue()
            @:return none
        """
        self._queue = LinkedList()

    @staticmethod
    def clone(list_queue_instance: 'ListQueue') -> 'ListQueue':
        """ Clone the queue
            Usage:  queue = ListQueue.clone(instance)
            @:param instance an ListQueue instance to deep copy data from.
            @:return a deep object copy of the queue
            @:raises TypeError if instance is provided and it is not an ListQueue instance
        """
        if list_queue_instance is not None and not isinstance(list_queue_instance, ListQueue):
            raise TypeError('Can only copy instances of ListQueue')

        queue = ListQueue()
        queue._queue = LinkedList.clone(list_queue_instance._queue)
        return queue


    def enqueue(self, item: Any) -> None:
        """ Enqueue an item onto the queue
            Usage:   queue.enqueue(item)
            @:param item to enqueue
            @:return none
        """
        self._queue.append(item)

    def dequeue(self) -> Any:
        """ Dequeue an item from the queue and return the item
            Usage:   item = queue.dequeue()
            @:return item that is dequeued
            @:raises IndexError if the queue is empty
        """
        if self.empty:
            raise IndexError('Queue is empty')

        item = self._queue.first 
        self._queue.remove_first() 
        return item


    def clear(self) -> None:
        """ Clear the queue
            Usage: array_queue.clear():
            @:return none
        """
        self._queue.clear()

    @property
    def front(self) -> Any:
        """ Get the item at the front of the queue
            Usage:   item = queue.front
            @:return item that is in the front
            @:raises IndexError if the queue is empty
        """
        if self._queue.empty:
            raise IndexError('Queue is empty')

        return self._queue.first

    @property
    def empty(self) -> bool:
        """ Check whether the queue is empty
            Usage:   empty = queue.empty
            @:return empty boolean as to whether the queue is empty
        """
        return self._queue.empty

    def __eq__(self, other: 'ListQueue') -> bool:
        """ Equality operator ==
            Usage: array1 == array2
            @:param other the instance to compare self to
            @:return true if the arrays are equal (deep check)
        """
        if type(other) != type(self):
            return False

        return self._queue == other._queue

    def __len__(self) -> int:
        """ len operator for getting length of the queue
            Usage: length = len(queue)
            @:return the length of the Queue (number of items on the queue)
        """
        return len(self._queue)

    def __str__(self) -> str:
        """ Return a string representation of the data and structure
            Usage: print(queue):
            @:return str the string representation of the data and structure
        """
        return f'ListQueue: Top at front.  Queue: {self._queue}'
