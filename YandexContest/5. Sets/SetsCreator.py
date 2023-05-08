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


class GraphNode:
    children = dict()
    parents = dict()\


    def __init__(self, id_key):
        self.id_key = id_key
        self.children = dict()
        self.parents = dict()
        self.overwrited = False
        self.new_identity = None

    def __str__(self):
        return f'Graph with ID {self.id_key}'

    def perform_safe(self, funk, arg):

        def perform():
            if not self.overwrited:
                return funk(self, arg)
            else:
                return self.new_identity.funk(self.new_identity, arg)

        return perform()

    def add_child(self, child):
        return self.perform_safe(GraphNode.add_child_wrapped, child)

    def remove_child(self, child):
        return self.perform_safe(GraphNode.remove_child_wrapped, child)

    def add_parent(self, parent):
        return self.perform_safe(GraphNode.add_parent_wrapped, parent)

    def remove_parent(self, parent):
        return self.perform_safe(GraphNode.remove_parent_wrapped, parent)

    def add_child_wrapped(self, child):
        self.children[child.id_key] = child

    def remove_child_wrapped(self, child):
        self.children.pop(child.id_key)

    def add_parent_wrapped(self, parent):
        self.parents[parent.id_key] = parent

    def remove_parent_wrapped(self, parent):
        self.parents.pop(parent.id_key)

    @staticmethod
    def create_new_graph():
        universum = GraphNode(0)
        return universum


def create_answer(requirements):
    universum = GraphNode.create_new_graph()
    universum.add_child(GraphNode(1))
    print(str(universum.children[1]))

create_answer('balls')

size_of_input_table, input_table = read_input('nc_inp1.txt')


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