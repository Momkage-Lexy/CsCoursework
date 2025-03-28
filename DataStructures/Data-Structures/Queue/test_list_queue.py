import unittest

from list_queue import ListQueue

class ListQueueTest(unittest.TestCase):
    def setUp(self):
        self._queue = ListQueue()

    def test_01_enqueue_should_insert_item_at_the_front_of_queue(self):
        self._queue.enqueue(7)
        assert self._queue.front == 7

    def test_02_enqueue_should_update_size_property(self):
        test_length = 3
        for i in range(test_length):
            self._queue.enqueue(i)
        
        assert len(self._queue) == test_length

    def test_03_dequeue_should_return_value_stored_at_front_of_queue(self):
        self._queue.enqueue(7)
        self._queue.enqueue(17)

        front_item = self._queue.front
        assert self._queue.dequeue() == front_item    

    def test_04_dequeue_should_update_size_property(self):
        enqueuelength = 5
        for i in range(enqueuelength):
            self._queue.enqueue(i)
        
        dequeuelength = 2
        for i in range(dequeuelength):
            self._queue.dequeue()

        assert len(self._queue) == (enqueuelength - dequeuelength)

    def test_05_dequeue_should_raise_IndexError_if_attempting_to_dequeue_empty_list(self):
        with self.assertRaises(IndexError):
            item = self._queue.dequeue()

    def test_06_dequeue_last_item_should_result_in_empty_list(self):
        self._queue.enqueue(5)
        self._queue.dequeue()

        assert self._queue.empty == True

    def test_07_clear_should_result_in_empty_list(self):
        enqueuelength = 5
        for i in range(enqueuelength):
            self._queue.enqueue(i)
        
        self._queue.clear()
        assert self._queue.empty == True

    def test_08_clear_should_update_size_property(self):
        self._queue.enqueue(1)
        self._queue.enqueue(2)

        self._queue.clear()

        assert len(self._queue) == 0

    def test_09_queue_should_maintain_data_integrity_after_multiple_operations(self):
        for i in range(5):
                self._queue.enqueue(i)

        for i in range(5):
            assert i == self._queue.dequeue()

if __name__ == '__main__':
    unittest.main()