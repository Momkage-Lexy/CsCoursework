import unittest
import pytest
from linkedlistadt import LinkedList


class LinkedListTest(unittest.TestCase):

    def setUp(self):
        self._linked_list: LinkedList = LinkedList()

        for i in range(10):
            self._linked_list.append(i)
    
    @staticmethod
    def check_data_integrity(linked_list: LinkedList) -> bool: # pragma: no cover
        count = 0
        for item in linked_list:
            if item != count:
                return False
            count += 1

        count = len(linked_list) - 1
        node = linked_list.tail
        while node is not None:
            if node.item != count:
                return False
            node = node.previous
            count -= 1

        return True
    
    def test_01_insert_before_at_head_should_insert_a_node_with_correct_data_to_beginning(self):
        ''' Inserting a value before the head node, aka index 0, should update the linkedlist with a new value at the beginning'''
        values = [15, 25, 35, 45]
        linked_list = LinkedList()

        for item in values:
            linked_list.append(item)

        
        linked_list.insert_before(before_item=linked_list.head.item, new_item=5)

        assert linked_list[0] == 5

    def test_02_insert_before_at_head_should_update_head_attribute_with_correct_node(self):
        values = [15, 25, 35, 45]
        linked_list = LinkedList()

        for item in values:
            linked_list.append(item)

        old_head : LinkedList._Node = linked_list.head

        linked_list.insert_before(before_item=linked_list.first, new_item=5)

        assert linked_list.head != old_head

    def test_03_insert_before_at_tail_should_insert_a_node_with_correct_data_before_end(self):
        values = [5, 15, 25, 45]
        linked_list = LinkedList()

        for item in values:
            linked_list.append(item)

        linked_list.insert_before(before_item=linked_list.last, new_item=35)
        assert linked_list[3] == 35

    def test_04_insert_before_at_tail_should_keep_tail_preserved(self):
        values = [5, 15, 25, 45]
        linked_list = LinkedList()

        for item in values:
            linked_list.append(item)
        
        old_tail: LinkedList._Node = linked_list.tail

        linked_list.insert_before(before_item=linked_list.last, new_item=35)

        assert old_tail == linked_list.tail
    
    def test_05_insert_before_at_middle_should_insert_a_node_with_correct_data_to_middle(self):
        values = [5, 15, 25, 35, 45]
        linked_list = LinkedList()

        for i in values:
            linked_list.append(i)

        linked_list.insert_before(25, 20)

        new_values = [5, 15, 20, 25, 35, 45]
        i = 0
        for item in linked_list:
            self.assertEqual(new_values[i], item)
            i += 1

    def test_06_insert_before_subsequently_should_insert_correct_data_multiple_times(self):
        values = [5, 15, 25, 35, 45]
        linked_list = LinkedList()

        linked_list.append(values[4])
        for i in range(3, -1, -1):
            linked_list.insert_before(values[i + 1], values[i])

        i = 0

        for item in linked_list:
            self.assertEqual(values[i], item)
            i += 1
    
    def test_07_insert_before_should_update_len_property(self):
        old_length = len(self._linked_list)
        self._linked_list.insert_before(before_item=5,new_item=3.5)
        # the new length should be one larger than the old length (+1)
        assert len(self._linked_list) == (old_length + 1)

    def test_08_insert_before_at_nonexistent_item_should_raise_KeyError(self):
        values = [5, 15, 25, 35, 45]
        linked_list = LinkedList()

        linked_list.append(values[0])
        for i in values:
            linked_list.append(i)

        with pytest.raises(KeyError):
            linked_list.insert_before(500, 1000)

    def test_09_insert_before_on_empty_LinkedList_should_raise_KeyError(self):
        empty_linked_list = LinkedList()

        with pytest.raises(KeyError):
            empty_linked_list.insert_before(0, 1000)

    def test_10_insert_before_on_LinkedList_of_length_1_should_make_new_head(self):
        small_linked_list = LinkedList()
        small_linked_list.append(1)

        old_head_node: LinkedList._Node = small_linked_list.head

        small_linked_list.insert_before(before_item=1, new_item=0)

        assert old_head_node != small_linked_list.head
        assert small_linked_list.head.item == 0

    def test_11_insert_after_at_head_should_insert_a_node_with_correct_data_next_to_beginning(self):
        values = [5, 25, 35, 45]
        linked_list = LinkedList()

        for item in values:
            linked_list.append(item)

        linked_list.insert_after(after_item=linked_list.head.item, new_item=15)

        assert linked_list[1] == 15

    def test_12_insert_after_at_head_should_preserve_head_property(self):
        values = [5, 25, 35, 45]
        linked_list = LinkedList()
        for item in values:
            linked_list.append(item)

        old_head : LinkedList._Node = linked_list.head

        linked_list.insert_after(after_item=linked_list.head.item, new_item=15)

        assert linked_list.head == old_head

    def test_13_insert_after_at_tail_should_insert_a_node_with_correct_data_to_end(self):
        values = [5, 15, 25, 35]
        linked_list = LinkedList()
        for item in values:
            linked_list.append(item)
        
        linked_list.insert_after(after_item=linked_list.tail.item, new_item=45)

        assert linked_list[4] == 45
    
    def test_14_insert_after_at_tail_should_update_tail_attribute_with_correct_node(self):
        values = [5, 15, 25, 35]
        linked_list = LinkedList()
        for item in values:
            linked_list.append(item)
        
        old_tail: LinkedList._Node = linked_list.tail

        linked_list.insert_after(after_item=linked_list.tail.item, new_item=45)

        assert linked_list.tail != old_tail

    def test_15_insert_after_subsequently_should_insert_correct_data_multiple_times(self):
        values = [5, 15, 25, 35, 45]
        linked_list = LinkedList()

        linked_list.append(values[0])
        for i in range(1, 5):
            linked_list.insert_after(values[i - 1], values[i])

        i = 0

        for item in linked_list:
            self.assertEqual(values[i], item)
            i += 1
        
    def test_16_insert_after_should_update_len_property(self):
        old_length = len(self._linked_list)
        self._linked_list.insert_after(after_item=5,new_item=3.5)
        # the new length should be one larger than the old length (+1)
        assert len(self._linked_list) == (old_length + 1)
    
    def test_17_insert_after_at_middle_should_insert_a_node_with_correct_data_to_middle(self):
        values = [5, 15, 25, 35, 45]
        linked_list = LinkedList()

        for i in values:
            linked_list.append(i)

        linked_list.insert_after(25, 30)

        new_values = [5, 15, 25, 30, 35, 45]
        i = 0

        for item in linked_list:
            self.assertEqual(new_values[i], item)
            i += 1

    def test_18_insert_after_at_nonexistent_key_should_raise_KeyError(self):
        values = [5, 15, 25, 35, 45]
        linked_list = LinkedList()

        linked_list.append(values[0])
        for i in values:
            linked_list.append(i)

        with pytest.raises(KeyError):
            linked_list.insert_after(500, 1000)

    def test_19_insert_after_on_empty_LinkedList_should_raise_KeyError(self):
        empty_linked_list = LinkedList()

        with pytest.raises(KeyError):
            empty_linked_list.insert_before(0, 1000)

    def test_20_insert_after_on_LinkedList_of_length_1_should_make_new_tail(self):
        small_linked_list = LinkedList()
        small_linked_list.append(0)

        old_tail_node: LinkedList._Node = small_linked_list.tail

        small_linked_list.insert_after(after_item=0, new_item=1)

        assert old_tail_node != small_linked_list.tail
        assert small_linked_list.tail.item == 1
        