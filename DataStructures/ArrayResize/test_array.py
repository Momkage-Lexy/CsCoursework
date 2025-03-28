import unittest
# from tests.gradescope import *
from arrayadt import Array


class ArrayTest(unittest.TestCase):

    def setUp(self):
        self._array = Array(size=10)
        for i in range(len(self._array)):
            self._array[i] = i

    def test_01_resize_smaller_should_update_length_attribute(self):
        self._array.resize(5)
        assert len(self._array) == 5

    def test_02_resize_smaller_should_not_contain_data_at_index_equal_to_new_length(self):
        new_size = 5
        truncated_value = self._array[new_size]
        self._array.resize(new_size)
        
        for i in range(len(self._array)):
            assert truncated_value != self._array[i]

    def test_03_resize_smaller_should_not_contain_last_item_of_old_array(self):
        new_size = 5
        truncated_value = self._array[len(self._array) - 1]
        self._array.resize(new_size)

        for i in range(len(self._array)):
            assert truncated_value != self._array[i]

    def test_04_resize_smaller_should_retain_truncated_data(self):
        self._array.resize(5)

        for i in range(len(self._array)):
            assert i == self._array[i]

    def test_05_resize_larger_should_update_length_attribute(self):
        self._array.resize(15)
        assert len(self._array) == 15

    def test_06_resize_larger_retains_original_data(self):
        original_size = len(self._array)
        self._array.resize(15)

        for i in range(original_size):
            assert i in self._array    

    def test_07_resize_larger_keeps_data_at_original_index(self):
        old_size = len(self._array)
        new_size = 15
        self._array.resize(new_size)

        for i in range(old_size):
            assert i == self._array[i]
    
    def test_08_resize_larger_should_extend_array_with_none_values(self):
        old_size = len(self._array)
        new_size = 15
        self._array.resize(new_size)

        for i in range(old_size, new_size):
            assert self._array[i] is None
    
    def test_09_resize_to_same_size_should_be_same_length(self):
        old_length = len(self._array)
        self._array.resize(old_length)

        assert len(self._array) == old_length

    def test_10_resize_to_same_size_should_retain_original_data(self):
        old_length = len(self._array)
        self._array.resize(old_length)

        for i in range(old_length):
            assert i == self._array[i]
    
    