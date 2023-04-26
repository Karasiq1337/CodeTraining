

ap_rows = list()
qu_list = list()
with open('input.txt') as f:
    r_amount = int(f.readline().strip('\n'))
    for i in range(r_amount):
        ap_rows.append(f.readline().strip('\n').split('_'))
    qu_len = int(f.readline().strip('\n'))
    i = 0
    for i in range(qu_len):
        qu_list.append(f.readline().strip('\n').split(' '))


def dump_update(side, row, seats):
    if side == "left":
        r = 0
    else:
        r = 1
    for seat in seats:
        l = list(ap_rows[row][r])
        l[seat] = 'X'
        ap_rows[row][r] = "".join(l)
    for i in ap_rows:
        print(i[0] + '_' + i[1])
    for seat in seats:
        l = list(ap_rows[row][r])
        l[seat] = '#'
        ap_rows[row][r] = "".join(l)


def create_taken(side, row, seats):
    seats_letters = "ABCDEF"
    offset = 0
    if side == "right":
        offset = 3
    message = "Passengers can take seats:"
    for seat in seats:
        message += " " + str(row + 1) + seats_letters[offset + seat]
    print(message)
    dump_update(side, row, seats)

def check_qu(qu_request):
    can_take = False
    row_taken = int()
    seats_taken = list()
    i = 0
    if qu_request[1] == "left":
        side = 0
    else:
        side = 1
    if (qu_request[2] == "window" and qu_request[1] == "left") or (qu_request[2] == "aisle" and qu_request[1] == "right"):
        for i in range(len(ap_rows) + 1):
            if not can_take:
                if i > r_amount - 1:
                    break
                seats_taken.clear()
                j = 0
                for j in range(int(qu_request[0])):
                    if ap_rows[i][side][j] != '.':
                        can_take = False
                        break
                    else:
                        seats_taken.append(j)
                        can_take = True
                row_taken = i
            else:
                create_taken(qu_request[1], row_taken, seats_taken)
                return
        print("Cannot fulfill passengers requirements")
        return
    else:
        for i in range(len(ap_rows) + 1):
            if not can_take:
                if i > r_amount -1:
                    break
                seats_taken.clear()
                j = 2
                while j >= (3 - int(qu_request[0])):
                    if ap_rows[i][side][j] != '.':
                        can_take = False
                        break
                    else:
                        seats_taken.append(j)
                        can_take = True
                    j -= 1
                row_taken = i
            else:
                seats_taken.reverse()
                create_taken(qu_request[1], row_taken, seats_taken)
                return
        print("Cannot fulfill passengers requirements")
        return

for qu in qu_list:
    check_qu(qu)
