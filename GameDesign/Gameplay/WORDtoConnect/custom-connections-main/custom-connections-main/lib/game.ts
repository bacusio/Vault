import { GameOptions } from "./game-options";

/** The tailwind strings for each of the color categories. */
export const colors = [
  "bg-nyt-yellow",
  "bg-nyt-green",
  "bg-nyt-blue",
  "bg-nyt-purple",
] as const;

/**
 * Returns the color index of the given word. For example, if "light" is in the
 * yellow category, 0 will be supplied.
 */
export function getColor(options: GameOptions, word: string) {
  for (let i = 0; i < 4; i++) {
    if (options.words[i].includes(word)) {
      return i;
    }
  }

  return -1; // not found
}

/**
 * Returns whether all of the colors in the guess are of the same category.
 * @see {@link getColor}
 */
export function validateGuess(options: GameOptions, guess: string[]) {
  const first = getColor(options, guess[0]);

  for (let i = 1; i < 4; i++) {
    if (getColor(options, guess[i]) !== first) {
      return false;
    }
  }

  return true;
}

/**
 * Returns the maximum number of the same color in the given guess. For example,
 * a guess with 3 blue words and 1 yellow word would return 3. Primarily used
 * for displaying the "One away..." message to the user.
 * @see {@link getColor}
 */
export function getCommonColors(options: GameOptions, guess: string[]) {
  const numbers = [0, 0, 0, 0];

  for (let i = 0; i < 4; i++) {
    numbers[getColor(options, guess[i])]++;
  }

  let max = 0;
  for (let color = 0; color < 4; color++) {
    if (numbers[color] > max) {
      max = numbers[color];
    }
  }

  return max;
}

/**
 * Returns the total number of guesses that are considered mistakes.
 * @see {@link validateGuess}
 */
export function getTotalMistakes(options: GameOptions, guesses: string[][]) {
  let total = 0;

  for (let i = 0; i < guesses.length; i++) {
    if (validateGuess(options, guesses[i]) === false) {
      total++;
    }
  }

  return total;
}

/**
 * Returns a boolean array corresponding to the color categories that have
 * been revealed based on the user's previous guesses.
 * @see {@link validateGuess}, {@link getColor}
 */
export function getRevealedColors(options: GameOptions, guesses: string[][]) {
  const revealed = [false, false, false, false];

  for (let i = 0; i < guesses.length; i++) {
    if (validateGuess(options, guesses[i])) {
      revealed[getColor(options, guesses[i][0])] = true;
    }
  }

  return revealed;
}

/**
 * Returns a newly shuffled word pool based on the color categories that the
 * user has not yet guesses correctly.
 * @see {@link getRevealedColors}
 */
export function regenerateWordPool(options: GameOptions, guesses: string[][]) {
  const revealedColors = getRevealedColors(options, guesses);
  const wordPool: string[] = [];

  for (let i = 0; i < 4; i++) {
    if (revealedColors[i] === false) {
      options.words[i].forEach((word) => wordPool.push(word));
    }
  }

  wordPool.sort(() => Math.random() - 0.5);
  return wordPool;
}
