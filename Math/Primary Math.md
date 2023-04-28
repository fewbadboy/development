# Primary Math

## Greatest common factor(GCF)

- Using prime factorization to find the GCF

```math
36=2×2×3×3
48=2×2×2×2×3
```

the prime factors 2, 2, 3 in common, multiply these prime factors: 2 × 2 × 3 = 12

GCF(36, 48)=12

- Using Euclid's algorithm to find the GCF  

1. Divide the larger number by the smaller number. If the remainder is 0, the divisor is the GCF. If not continue to the next step.
2. Divide the smaller number (the previous divisor) by the remainder. If the new remainder is 0, the divisor is the GCF.
3. Continue the process of dividing the previous divisor by the remainder until there is no remainder. The divisor that results in a remainder of 0 is the GCF of the original two numbers

288/144=2...60 114/60=1...54 60/54=1...6 54/6=9...0

GCF(114, 288)=6

## Least common multiple(LCM)

- Using a table

    1. List the numbers vertically in a table.
    2. Divide each number by 2 (the first prime number). If any of the numbers divides evenly, write the result in the following column of the table, and write the divisor (in this case 2) at the top of the table. If it does not divide evenly, rewrite the number in the following column.
    3. Divide each subsequent column by 2 until none of the numbers can be evenly divided by 2. Continue this process for the next largest prime number until the quotient of all the numbers is equal to 1 (the last column is all 1s).
    4. Multiply the numbers in the top row (the prime factors). The product is the LCM.

    ||2|2|2|7|
    |-:|-:|-:|-:|-:|
    |7|7|7|7|1|
    |8|4|2|1|1|
    |14|7|7|7|1|

    LCM(7, 8, 14) = 2 × 2 × 2 × 7 = 56


- Euclid's algorithm

    LCM(a,b) = |a·b| / GCF(a,b)

## Golden ratio

a/b = (a + b)/a = c

c²-c-1=0  c 约 1.618

## Euler's Theorem

Faces + Vertices - Edges = 2
