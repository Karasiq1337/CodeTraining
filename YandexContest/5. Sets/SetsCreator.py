import time


def read_input(input_file):
    set_table = list()
    with open(input_file) as f:
        table_size = int(f.readline().strip('\n'))
        for i in range(table_size):
            set_table.append(list(f.readline().strip('\n')))
    return table_size, set_table


def measure_execution_time(func, args):

    def measure():
        t = time.process_time()
        result = func(args)
        elapsed_time = time.process_time() - t
        print('Execution time:', elapsed_time, 'seconds')
        return result

    return measure()


def check_if_correct(requirements, answer):
    for i in range(len(requirements)):
        for j in range(len(requirements[i])):
            if not set_fact_check(requirements[i][j], answer[i], answer[j]):
                return False
    return True


def set_fact_check(fact, set_i, set_j):
    operations = {
        '=': lambda i, j: i == j,
        '<': lambda i, j: i.issubset(j),
        '>': lambda i, j: j.issubset(i),
        '!': lambda i, j: not i.issubset(j),
        '^': lambda i, j: not j.issubset(i),
        '?': lambda i, j: True
    }
    return operations[fact](set_i, set_j)


def input_contradiction_check(input_table):
    self_contradictorily = frozenset(['!','^'])

    def contradiction_check(ij, ji):
        contradictions = {
            '=': frozenset(['!', '^']),
            '<': frozenset(['^']),
            '>': frozenset(['!']),
            '!': frozenset(['>']),
            '^': frozenset(['<']),
            '?': frozenset()
        }
        return ji in contradictions[ij]

    start = 0
    end = len(input_table[0])
    for i in range(len(input_table)):
        j = start
        for j in range(end):
            if i == j and input_table[i][j] in self_contradictorily:
                return False
            if contradiction_check(input_table[i][j], input_table[j][i]):
                return False
        start += 1
    return True


def read_output(output_file, sets_amount):
    with open(output_file) as f:
        if f.readline().strip('\n') == "No":
            return False
        else:
            d = dict()
            for i in range(sets_amount):
                output_str_list = list(map(int, f.readline().strip('\n').split(' ')))
                j = 1
                s = set()
                for j in range(1, output_str_list[0]+1):
                    s.add(output_str_list[j])
                d[i] = s
    return d


def check_if_mach(input_file, output_file):
    size_of_input_table, input_table = read_input(input_file)
    output = read_output(output_file, size_of_input_table)
    if output is False:
        if input_contradiction_check(input_table) is False:
            return True
        else:
            return False
    return check_if_correct(input_table, output)


class Universum:
    children = dict()
    all_sets = dict()

    def __init__(self, sets_amount):
        for i in range(sets_amount):
            self.set_id = 0
            new_node = SetGraph(i+1, self)
            self.all_sets[i+1] = new_node
            self.children[i+1] = new_node
            new_node.parents[0] = self

    def __str__(self):
        return f'Universum with {self.all_sets.keys()} sets and {self.children.keys()} direct children'

    def remove_child(self, child):
        self.children.pop(child.set_id)
        child.parents.pop(self.set_id)

    def make_equal(self, first_set, second_set):
        extra_parents = second_set.parents.keys() - first_set.parents.keys()
        extra_children = second_set.children.keys() - first_set.children.keys()
        for extra_parent in extra_parents:
            first_set.add_parent(second_set.parents[extra_parent])
        for extra_child in extra_children:
            first_set.add_child(second_set.children[extra_child])
        second_set_parents = list(second_set.parents.keys())
        for parent in second_set_parents:
            second_set.remove_parent(second_set.parents[parent])
        second_set_children = list(second_set.children.keys())
        for child in second_set_children:
            second_set.remove_child(second_set.children[child])
        self.all_sets[second_set.set_id] = first_set

    def find_child(self, child_id):
        if child_id in self.children:
            return self.children[child_id]
        return None

    def get_identity(self, set_arg):
        return self.all_sets[set_arg.set_id]


class SetGraph:
    def __init__(self, set_id, universum):
        self.set_id = set_id
        self.parents = dict()
        self.children = dict()
        self.universum = universum

    def __str__(self):
        return f'Set with {self.set_id} Id'

    def get_self_identity(self):
        return self.universum.get_identity(self)

    def remove_child(self, child):
        actual_identity = self.get_self_identity()
        actual_identity.children.pop(child.set_id)
        child.parents.pop(actual_identity.set_id)

    def remove_parent(self, parent):
        actual_identity = self.get_self_identity()
        actual_identity.parents.pop(parent.set_id)
        parent.children.pop(actual_identity.set_id)

    def add_child(self, child):
        actual_identity = self.get_self_identity()
        actual_identity.children[child.set_id] = child
        child.parents[actual_identity.set_id] = actual_identity

    def add_parent(self, parent):
        actual_identity = self.get_self_identity()
        actual_identity.parents[parent.set_id] = parent
        parent.children[actual_identity.set_id] = actual_identity

    def find_parent(self, parent_id):
        actual_identity = self.get_self_identity()
        if parent_id in actual_identity.parents:
            return actual_identity.parents[parent_id]
        return None

    def find_child(self, child_id):
        actual_identity = self.get_self_identity()
        if child_id in actual_identity.children:
            return actual_identity.children[child_id]
        return None

        

def create_answer(requirements):
    uni = Universum(len(requirements))
    first = uni.find_child(1)
    second = uni.find_child(2)
    third = uni.find_child(3)
    forth = uni.find_child(4)
    first.add_child(second)
    uni.make_equal(third, second)
    second.add_child(forth)
    return second


size_of_input_table, input_table = read_input('nc_inp1.txt')
ans = create_answer(input_table)
print(ans)


matching_pairs = [['nc_inp1.txt', 't_out1.txt'],
                  ['c_inp1.txt', 'c_out.txt'],
                  ['c_inp2.txt', 'c_out.txt'],
                  ['nc_inp2.txt', 't_out2_2.txt'],
                  ['nc_inp2.txt', 't_out2_2.txt'],
                  ['nc_inp2.txt', 't_out2_3.txt'],
                  ['nc_inp2.txt', 't_out2_4.txt'],
                  ['nc_inp3.txt', 't_out3.txt']]


not_matching_pairs = ['nc_inp2.txt', 'f_out2.txt']


'''for i in matching_pairs:
    print(check_if_mach(i[0], i[1]))'''
