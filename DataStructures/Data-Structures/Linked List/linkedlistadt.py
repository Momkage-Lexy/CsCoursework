from typing import Any


class LinkedList:
    """ Class LinkedList - representing an unordered linked list
        Depends on LinkedList._ListNode class to store the items, previous, and next nodes.
            Stipulations:
            1. Must manage the linked list using only two LinkedList._ListNode objects (_head and _tail)
            2. Must adhere to the docstring requirements per method, including raising
               raising appropriate exceptions where indicated.
            3. The LinkedList can contain duplicate items.
    """

    class _Node:
        """ _LinkedList._ListNode - private class that represents a node in a linked list
                Stipulations:
                1. Must adhere to the docstring requirements per method, including raising
                raising appropriate exceptions where indicated.
        """

        def __init__(self, item, previous_node: 'LinkedList._Node' = None, next_node: 'LinkedList._Node' = None) -> None:
            """ Constructor for the Node
                Usage:  node = _Node() or node = _Node(None, None) or node = _Node(previous_node, next_node)
                @:param item the item (data) to store in the node
                @:param previous_node the corresponding previous node of this node in the linked list
                @:param next_node the corresponding next node of this node in the linked list
                @:return none
            """
            self._item = item
            self._next = next_node
            self._previous = previous_node


        @property
        def item(self) -> Any:
            """ Getter for the item
                Usage: item = node.item
                @:return the item stored in the node
            """
            return self._item
        
        @item.setter
        def item(self, item: Any) -> None:
            """ Setter for the item
                Usage: node.item = item
                @:param item the item to store in the node
                @:return none
            """
            self._item = item

        @property
        def previous(self) -> 'LinkedList._Node':
            """ Getter for the previous node
                Usage: previous_node = node.previous
                @:return the previous node of the node
            """
            return self._previous


        @previous.setter
        def previous(self, previous_node: 'LinkedList._Node') -> None:
            """ Setter for the previous node
                Usage: node.previous = previous
                @:param previous_node the node's previous_node instance
                @:return none
            """
            self._previous = previous_node

        @property
        def next(self) -> 'LinkedList._Node':
            """ Getter for the next node
                Usage: next_node = node.next
                @:return the next node of the node
            """
            return self._next

        @next.setter
        def next(self, next_node: 'LinkedList._Node') -> None:
            """ Setter for the next node
                Usage: node.next = next_node
                @:param next_node the node's next_node instance
                @:return none
            """
            self._next = next_node

        def __eq__(self, other: 'LinkedList._Node') -> bool:
            """ Equality operator ==
                Usage: array1 == array2
                @:param other the instance to compare self to
                @:return true if the arrays are equal (deep check)
            """
            if type(other) != type(self):
                return False

            return id(self) == id(other)
        
        def __neq__(self, other: 'LinkedList._Node') -> bool:
            return not self.__eq__(self, other)


        def __str__(self) -> str:
            """ Return a string representation of the data and structure
                Usage: print(node):
                @:return str the string representation of the data and structure
            """
            return f'LinkedList._Node with Item: {self._item}, Previous: {self._previous}, Next: {self._next}'

    def __init__(self, python_list_instance: list = None) -> None:
        """ Constructor for the LinkedList
            Usage:  1. linked_list = LinkedList()
                    2. linked_list = LinkedList(['This', 'is', 'a', 'Python', 'list'])
            @:return none
            @:raises TypeError if optional python_list_instance provided is not a list
        """
        self._head: LinkedList._Node = None
        self._tail: LinkedList._Node = None
        self._count: int = 0

        if python_list_instance is not None:
            if not isinstance(python_list_instance, list):
                raise TypeError('LinkedList being initialized with an instance that is not a Python list')
            
            for item in python_list_instance:
                self.append(item)


    @classmethod
    def clone(cls, linked_list_instance: 'LinkedList') -> 'LinkedList':
        """ Clone the LinkedList
            Usage:  new_linked_list = LinkedList.clone(instance)
            @:param instance a LinkedList instance to deep copy data from.
            @:return a deep object copy of the linked list
            @:raises TypeError if instance is provided and it is not an LinkedList instance
        """
        if not isinstance(linked_list_instance, LinkedList):
            raise TypeError('Instance is not a LinkedList')
        
        linked_list = cls()

        for item in linked_list_instance:
            linked_list.append(item)

        return linked_list


    def append(self, item: Any) -> None:
        """ Append an item to the end of the list
            Usage: linked_list.append(item)
            @:param item the desired item to append to the linked list
            @:return none
        """
        self._tail = LinkedList._Node(item, previous_node=self._tail)
        if self._head is None:
            self._head = self._tail
        else:
            self._tail.previous.next = self._tail

        self._count += 1


    def prepend(self, item: Any) -> None:
        """ Prepend an item to the end of the list
            Usage: linked_list.prepend(item)
            @:param item the desired item to prepend to the linked list
            @:return none
        """
        self._head = LinkedList._Node(item, next_node=self._head)
        if self._tail is None:
            self._tail = self._head
        else:
            self._head.next.previous = self._head

        self._count += 1

# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
# Write in the following two functions: # # # # # # # # # # # # # # # # # # # # # #
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #

    def insert_before(self, before_item: Any, new_item: Any) -> None:
        """ Insert a new item before a specified item
            Usage: linked_list.insert_before(before_item, new_item)
            @:param before_item the item that the user wishes to insert before
            @:param new_item the desired item to insert
            @:return none
            @:raises KeyError if before_item is not found
        """
        if self._head is None:  # List is empty
            raise KeyError(f"Item {before_item} not found in the list.")

        # Search for the node to insert before
        current = self._head
        while current is not None and current.item != before_item:
            current = current.next

        if current is None:
            raise KeyError(f"Item {before_item} not found in the list.")

        # Insert new node before the found node
        new_node = LinkedList._Node(new_item, previous_node=current.previous, next_node=current)
        if current.previous is not None:
            current.previous.next = new_node
        else:
            self._head = new_node
        current.previous = new_node

        self._count += 1

    def insert_after(self, after_item: Any, new_item: Any) -> None:
        """ Insert a new item after a specified item
            Usage: linked_list.insert_after(after_item, new_item)
            @:param before_item the item that the user wishes to insert before
            @:param new_item the desired item to insert
            @:return none
            @:raises KeyError if before_item is not found
        """
            
        if self._head is None:  # List is empty
            raise KeyError(f"Item {after_item} not found in the list.")

        # Search for the node to insert after
        current = self._head
        while current is not None and current.item != after_item:
            current = current.next

        if current is None:
            raise KeyError(f"Item {after_item} not found in the list.")

        # Insert new node after the found node
        new_node = LinkedList._Node(new_item, previous_node=current, next_node=current.next)
        if current.next is not None:
            current.next.previous = new_node
        else:
            self._tail = new_node
        current.next = new_node

        self._count += 1

    def __getitem__(self, index: int) -> Any:
        """ Bracket operator for getting an item
            Usage: val = linked_list[0]
            @:param index the desired index
            @:return the item at the index
            @:raises IndexError if the index is out of bounds
        """
        if index >= len(self):
            raise IndexError(f'LinkedList index {index} is out of range')

        i = 0
        travel = self._head

        while travel is not None and i != index:
            travel = travel.next
            i += 1
        
        return travel.item


    def __setitem__(self, index: int, item: Any) -> None:
        """ Bracket operator for setting an item
            Usage: linked_list[index] = val
            @:param index the desired index to set
            @:param item the desired item to set at index
            @:raises IndexError if the index is out of bounds
            @:return none
        """
        if index >= len(self):
            raise IndexError(f'LinkedList index {index} is out of range')

        i = 0
        travel = self._head

        while travel is not None and i != index:
            travel = travel.next
            i += 1
        
        travel.item = item


    @property
    def head(self) -> 'LinkedList._Node':
        """ Return the LinkedList._ListNode instance pointing at the head of the linked list.
            Note: this method should be used for debug and test purposes only.
            Usage: head = linked_list.head
            @:return head the LinkedList._ListNode instance representing the head of the linked list
        """
        return self._head

    @property
    def tail(self) -> 'LinkedList._ListNode':
        """ Return the LinkedList._ListNode instance pointing at the tail of the linked list.
            Note: this method should be used for debug and test purposes only.
            Usage: tail = linked_list.tail
            @:return tail the LinkedList._ListNode instance representing the tail of the linked list
        """
        return self._tail

    @property
    def first(self) -> Any:
        """ Return the item at the head of the linked list.
            Usage: first_item = linked_list.first
            @:return first the item stored in the head of the list
            @:raises IndexError if the list is empty
        """
        return self._head.item


    @property
    def last(self) -> Any:
        """ Return the item at the tail of the linked list.
            Usage: last_item = linked_list.last
            @:return last the item stored in the tail of the list
            @:raises IndexError if the list is empty
        """
        if self._tail is None:
            raise IndexError('List is empty')

        return self._tail.item

    @property
    def empty(self) -> bool:
        """ Property to determine whether the list is empty
            @:return bool whether the list is empty
        """
        return self._count == 0

    def remove_first(self) -> None:
        """ Remove the first item in the linked list
            Usage: linked_list.remove_first()
            @:raises IndexError if the list is empty
        """
        if self.empty:
            raise IndexError('List is empty')

        self._head = self._head.next

        if self._head is not None:
            self._head.previous = None
        else:
            self._tail = None

        self._count -= 1

    def remove_last(self) -> None:
        """ Remove the last item in the linked list
            Usage: linked_list.remove_last()
            @:raises IndexError if the list is empty
        """
        if self.empty:
            raise IndexError('List is empty')

        self._tail = self._tail.previous

        if self._tail is not None:
            self._tail.next = None
        else:
            self._head = None

        self._count -= 1

    def __contains__(self, item: Any) -> bool:
        """ Equality operator ==
            Usage: if item in linked_list:
            @:param item the item to search for
            @:return true if the linked list contains the item
        """
        for iter in self:
            if iter == item:
                return True

        return False

    def __eq__(self, other: 'LinkedList') -> bool:
        """ Equality operator ==
            Usage: list1 == list2
            @:param other the instance to compare self to
            @:return true if the lists are equal (deep check)
        """
        if not isinstance(other, LinkedList):
            return False

        self_travel = self._head
        other_travel = other._head

        if len(self) != len(other):
            return False

        while self_travel is not None and other_travel is not None:
            if self_travel.item != other_travel.item:
                return False
            self_travel = self_travel.next
            other_travel = other_travel.next

        return True

    def __iter__(self) -> Any:
        """ Iterator operator
            Usage: for item in list:
            @:return yields the item at LinkedList._ListNode
        """
        node: LinkedList._Node = self._head
        while node is not None:
            yield node.item
            node = node.next

    def __reversed__(self) -> Any:
        """ Reversed iterator operator
            Usage: for item in reversed(list):
            @:return yields the item at LinkedList._ListNode
        """
        node: LinkedList._Node = self._tail
        while node is not None:
            yield node.item
            node = node.previous

    def __len__(self) -> int:
        """ len operator for getting length of the linked list
            Usage: size = len(linked_list)
            @:return the length of the LinkedList
        """
        return self._count

    def __str__(self) -> str:
        """ Return a string representation of the data and structure
            Usage: print(linked_list):
            @:return str the string representation of the data and structure
        """

        if self._count == 0:
            return '[]'

        string: str = '['
        sep: str = ' <-> '

        for item in self:
            string += str(item) + sep

        string = string[:len(string) - len(sep)]
        string += ']'

        return string