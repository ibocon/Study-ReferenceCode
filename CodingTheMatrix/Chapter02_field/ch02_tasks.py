#Task 2.4.10
import image

def T2410(filename):
    data = image.file2image(filename)
    pts = set()
    for x in range(166):
        for y in range(189):
            if data[y][x][0] < (0.5*255):
                pts.add(x-(y-189)*1j)
    return pts

#Task 2.4.17
import math
def T2417():
    n=20
    pts = set()
    w=(math.e)**(2*math.pi*1j)
    pts.add(w)
    for x in range(1,n):
        pts.add(math.e**((2*math.pi*1j)/x))
    return pts
