
initial_array = list()
with open('input.txt') as f:
    p = f.readline().strip('\n').split(' ')
    n = int(p[0])
    k = int(p[1])
    arr = f.readline().strip('\n').split(' ')
    i = 0
    for i in range(n):
        initial_array.append(int(arr[i]))


def find_distance(k, initial_arr):
    sorted_list = sorted(initial_arr)
    result_dist = {}
    sub_arr_start, sub_arr_end = 0, sum(sorted_list[:k+1])
    leftest_elem, rightest_elem = 0, len(sorted_list)
    for i,v in enumerate(sorted_list):
        sub_arr_end -= v
        result_dist[v] = (i-leftest_elem)*v*2 - sub_arr_start + sub_arr_end - k*v
        while leftest_elem < i and leftest_elem+k+1 < rightest_elem:
            sum_dist = (i-leftest_elem-1)*v*2 - sub_arr_start + sorted_list[leftest_elem] + sub_arr_end + sorted_list[leftest_elem+k+1] - k*v
            if sum_dist > result_dist[v]:
                break
            result_dist[v] = sum_dist
            sub_arr_start -= sorted_list[leftest_elem]
            leftest_elem += 1
            sub_arr_end += sorted_list[leftest_elem+k]
        sub_arr_start += v

    return [str(result_dist[v]) for v in initial_arr]

result = find_distance(k, initial_array)
for i in result:
    print(i)


'''
def create_dict(ar):
    d = dict()
    for i in ar:
        if i not in d.keys():
            d[i] = 0
        else:
            d[i] += 1
    return d


def faster_find_dist(root):
    current = root
    result = dict()
    while current is not None:
        result[current.value] = find_for_one(current)
        current = current.next
    return result


def find_for_one(current):
    st = time.time()
    distance = 0
    i_amount = d[current.value]
    l_node = current.prev
    l_delta = sys.maxsize
    r_node = current.next
    r_delta = sys.maxsize
    r_is_none = False
    l_is_none = False
    while i_amount < k:
        if l_node is not None:
            l_delta = abs(current.value - l_node.value)
        else:
            l_is_none = True
            l_delta = sys.maxsize
        if r_node is not None:
            r_delta = abs(current.value - r_node.value)
        else:
            r_is_none = True
            r_delta = sys.maxsize
        if (not l_is_none) and (l_delta <= r_delta):
            iteration_amount = d[l_node.value] + 1
            l_node = l_node.prev
            iteration_delta = l_delta
        elif not r_is_none:
            iteration_amount = d[r_node.value] + 1
            r_node = r_node.next
            iteration_delta = r_delta
        else:
            raise Exception("k > n")
        if (i_amount + iteration_amount) > k:
            distance += iteration_delta * (k - i_amount)
        else:
            distance += iteration_delta * iteration_amount
        i_amount += iteration_amount
    end = time.time() - st
    print('Took ' + str(end) + ' for ' + str(current.value))
    return distance


class ListNode(object):
    def __init__(self, v, pre):
        self.prev = pre
        self.value = v
        self.next = None


def find_in_list(v):
    current = r
    while current.value != v:
        current = current.next
    return current


def test_for(rand, size):
    x = random.randint(rand, size=size)
    d = create_dict(x)
    st = time.time()
    r = create_linked_l(list(d.keys()))
    elapsed_time = time.time() - st
    print('Execution time of creating l_list:', elapsed_time, 'seconds')
    st = time.time()
    result = faster_find_dist(r)
    elapsed_time = time.time() - st
    print('Execution time of searching:', elapsed_time, 'seconds')


def search_dist():
    result = faster_find_dist(r)
    for i in range(len(initial_array)):
        print(result[initial_array[i]])


def create_linked_l(initial):
    initial.sort()
    root = ListNode(initial[0], None)
    current = root
    for i in range(1, len(initial)):
        current.next = ListNode(initial[i], current)
        current = current.next
    return root


#d = create_dict(initial_array)
#r = create_linked_l(list(d.keys()))
#search_dist()

'''

