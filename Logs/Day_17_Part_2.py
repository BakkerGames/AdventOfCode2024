import pygame
from queue import PriorityQueue
import random
import time

# ----------------------------------- PARAMETERS

input_str = "2,4,1,1,7,5,4,6,0,3,1,4,5,5,3,0"
# input_str = "2,4,1,7,7,5,0,3,4,4,1,7,5,5,3,0"

time_step_ms = 250
start_delay_ms = 1000

# ----------------------------------- SOLVE

def combo_value(operand, a, b, c):
	if operand <= 3: return operand
	if operand == 4: return a
	if operand == 5: return b
	if operand == 6: return c

def run(program, a):
	b = 0
	c = 0
	output = []

	i = 0
	while i < len(program):
		opcode, operand = program[i], program[i+1]
		combo = combo_value(operand, a, b, c)

		if opcode == 0:	a = int(a / pow (2, combo))
		if opcode == 1: b = b ^ operand
		if opcode == 2: b = combo % 8
		if opcode == 3 and a != 0: i = operand - 2
		if opcode == 4: b = b ^ c
		if opcode == 5: output.append(combo % 8)
		if opcode == 6: b = int(a / pow (2, combo))
		if opcode == 7: c = int(a / pow (2, combo))

		i += 2

	return output

def step():
	global prev_digit_possibilities
	global digits_found_count

	q = PriorityQueue()
	for j in range(len(program)):
		if len(prev_digit_possibilities) == 1:
			digits_found_count = j
		for prev in prev_digit_possibilities:
			# add all 8 possibilities for the next digit of a, example: 2 => 20 to 27 (in octal)
			for i in range(8):
				q.put(prev*8 + i)

		# digit added so clear previous possibilities
		prev_digit_possibilities = []
		while not q.empty():
			a = q.get()
			output = run(program, a)
			n = len(output)
			# if the n digits of output don't match the last n digits of program, the possibility is ruled out
			if output == program[-n:]:
				prev_digit_possibilities.append(a)
				if n == 16:
					digits_found_count = 16
					q.queue.clear()
			q_copy = []
			for e in q.queue: q_copy.append(e)
			yield a, prev_digit_possibilities, q_copy

program = [int(s) for s in input_str.split(",")]

# contains the possible values of a for the n first digits, matching the n last digits of program 
prev_digit_possibilities = [0]

# Solve and return a generator to step through the states for display
steps = step()
digits_found_count = 0

# ----------------------------------- UI

# Initialize Pygame
pygame.init()

# Set screen dimensions
screen_width = 640
screen_height = 1000
screen = pygame.display.set_mode((screen_width, screen_height))

# Define possible digits (0-7 because octal)
dice_faces = [i for i in range(8)]
colors = {"white":(255, 255, 255), "black":(0, 0, 0), "red":(255, 0, 0), "green":(0, 255, 0), "blue":(100, 100, 255), "orange":(240, 220, 0)}

# Function to roll the dice
def roll_dice():
	return random.choice(dice_faces)

def render(text, color, y, x):
	screen.blit(font.render(text, True, colors[color]), (y, x))

def render16(text, cols, y, x):
	for char, col in zip(text, cols):
		screen.blit(font.render(char, True, colors[col]), (y, x))
		y += 28

# Main game loop
start_ms = time.time() * 1000 + start_delay_ms
prev_step = 0
a = ""
poss = []
q_copy = []

poss_cols = [["white" for _ in range(16)] for _ in range(8)]

# input("Press any key to start")
running = True
while running:
	for event in pygame.event.get():
		if event.type == pygame.QUIT:
			running = False

	# Calculate step to display
	step_count = int((time.time() * 1000 - start_ms) // time_step_ms)
	if prev_step < step_count:
		step_current = next(steps, None)
		if not step_current is None:
			prev_step = step_count

			# Grab data from solve step and convert to octal
			a, poss, q_copy = step_current
			a = oct(a)[2:]
			poss = [oct(e)[2:] for e in poss]
			q_copy = [oct(e)[2:] for e in q_copy]
			q_copy.sort()
			print(a, poss, q_copy)

		else:
			# end of "steps" generator
			step_count = prev_step

	# Clear the screen
	screen.fill((0, 0, 0))

	# Draw text
	font = pygame.font.Font(None, 30)
	render("step:", "white", 32, 24)
	render("output:", "white", 32, 56)
	render("answer:", "white", 32, 104)
	render("possible:", "white", 32, 156)
	# Draw step
	render(str(min(step_count, 240)), "white", 140, 24)

	# Draw output
	font = pygame.font.Font(None, 64)
	output = "".join(input_str.split(","))
	cols = []
	for digit_pos in range(16):
		if digit_pos < 16 - len(a):
			cols.append("white")
		else:
			cols.append("orange")
	render16(output, cols, 140, 48)

	# Draw answer
	answer = ""
	cols = []
	for digit_pos in range(16):
		if digit_pos < digits_found_count:
			answer += a[digit_pos]
			cols.append("green")
		elif digit_pos < len(a):
			answer += a[digit_pos]
			if a in poss:
				cols.append("orange")
			else:
				cols.append("red")
		else:
			answer += str(roll_dice())
			cols.append("white")
	render16(answer, cols, 140, 96)

	# Draw possibilities
	for digit_value in range(8):
		cols = []
		for digit_pos in range(16):
			if digit_pos < digits_found_count:
				cols.append("black")

			else:
				if digit_pos < len(a):
					# Set "poss_cols[digit_value][digit_pos]" to red if the digit in that spot is impossible
					digit_possible = False
					for v in poss + q_copy:
						# Check both in values possible and to be evaluated
						if str(digit_value) == v[digit_pos]:
							digit_possible = True
							break
					if not digit_possible:
						poss_cols[digit_value][digit_pos] = "red"
				cols.append(poss_cols[digit_value][digit_pos])

		render16([str(digit_value) for _ in range(16)], cols, 140, 106 + 40 * (digit_value+1))

	# Update the screen
	pygame.display.flip()

# Quit Pygame
# pygame.quit()