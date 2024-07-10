import { Primitive } from "zod";

/**
 * A compare function for sorting strings based on their case-insensitive
 * alphabetical order.
 */
export function alphabetical(a: string, b: string) {
  return a.toLowerCase() < b.toLowerCase() ? -1 : 1;
}

/** Returns an array from 0 (inclusive) to `length` (exclusive). */
export function range(length: number) {
  return Array.from({ length }, (_, i) => i);
}

/**
 * Returns a cloned array with elements `a` and `b` swapped, where `a` and `b`
 * are indexes into the array.
 */
export function toSwapped<T>(arr: T[], a: number, b: number) {
  const newArr: T[] = [];

  for (let i = 0; i < arr.length; i++) {
    if (i === a) {
      newArr[i] = arr[b];
    } else if (i === b) {
      newArr[i] = arr[a];
    } else {
      newArr[i] = arr[i];
    }
  }

  return newArr;
}

/**
 * Returns whether the two arrays contain the same values by sorting and
 * converting them into strings. This only works for arrays that contain
 * primitive values like numbers or strings.
 */
export function hasSameElements<T extends Primitive>(a: T[], b: T[]) {
  return a.sort().join(",") === b.sort().join(",");
}
