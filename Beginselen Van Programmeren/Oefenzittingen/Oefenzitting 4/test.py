__author__ = 'Wout De Puysseleir'

def returnTuple():
    return (2,8,5698)

def inputTuple(t):
    t += 5
    return t

values = ["One", "Two", "Three", "Four", "Five"]
newList = values[3:5]

for i in range(len(newList)):
    print(newList[i])

print()
print()
print()

for el in values:
    print(el)

Val1 = 0
Val2 = 0
Val3 = 0

(Val1, Val2, Val3) = returnTuple()

print()
print()
print()

print("test %s %s" %("word1", "word2"))
