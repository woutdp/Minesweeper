def ConvertToProperTimeFormat(x):
    minutes = x // 1
    seconds = x % 1
    seconds *= 60
    return str(int(minutes)) + " minutes " + str(int(seconds)) + " seconds "

def PrintMinMidMaxSpeech(minL, maxL):
    midL = round((minL+ maxL)/2, 2)
    print("min:", ConvertToProperTimeFormat(minL), "\nmid:", ConvertToProperTimeFormat(midL), "\nmax:", ConvertToProperTimeFormat(maxL))

minL = float(input("Input your minimum length in minutes: "))
maxL = float(input("Input your maximum length in minutes: "))
PrintMinMidMaxSpeech(minL,maxL)

