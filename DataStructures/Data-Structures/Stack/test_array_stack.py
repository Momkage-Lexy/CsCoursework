import unittest

from array_stack import ArrayStack

class ArrayStackTest(unittest.TestCase):
    def setUp(self):
        self._stack = ArrayStack(5)
    
    def test_01_push_should_insert_item_at_the_top_of_stack(self):
        self._stack.push(7)
        assert self._stack.top == 7

    def test_02_push_should_update_size_property(self):
        test_length = 3
        for i in range(test_length):
            self._stack.push(i)
        
        assert len(self._stack) == test_length

    def test_03_push_should_raise_IndexError_if_attempting_to_insert_to_a_full_stack(self):
        """20. Stack should raise IndexError when pushing onto a full stack"""
        with self.assertRaises(IndexError):
            for i in range(6):
                self._stack.push(i)
    
    def test_04_pop_should_return_value_stored_in_top_item(self):
        self._stack.push(7)
        self._stack.push(17)

        top_item = self._stack.top
        assert self._stack.pop() == top_item        
    
    def test_05_pop_should_update_size_property(self):
        pushlength = 5
        for i in range(pushlength):
            self._stack.push(i)
        
        poplength = 2
        for i in range(poplength):
            self._stack.pop()

        assert len(self._stack) == (pushlength - poplength)

    def test_06_pop_should_raise_IndexError_if_attempting_to_pop_off_an_empty_stack(self):
        with self.assertRaises(IndexError):
            item = self._stack.pop()

    def test_07_pop_last_item_should_result_in_empty_stack(self):
        """ Popping the last item on the stack should result in an empty stack"""
        self._stack.push(5)
        self._stack.pop()

        assert self._stack.empty == True
    
    def test_08_clear_should_result_in_an_empty_stack(self):
        """8. Clearing a stack should result in an empty stack"""
        self._stack.push(1)
        self._stack.push(2)

        self._stack.clear()

        assert self._stack.empty == True
    
    def test_09_clear_should_update_len(self):
        """9. Clearing a stack should result in a len of 0"""
        self._stack.push(1)
        self._stack.push(2)

        self._stack.clear()

        assert len(self._stack) == 0
    
    def test_10_stack_data_integrity(self):
        """Stack should maintain data integrity"""
        for i in range(5):
            self._stack.push(i)

        for i in range(4, -1, -1):
            assert i == self._stack.pop()

if __name__ == '__main__':
    unittest.main()