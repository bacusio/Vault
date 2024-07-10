import { Dispatch, SetStateAction } from "react";

type WordInputProps = {
  color: string;
  i: number;
  words: string[];
  setWords: Dispatch<SetStateAction<string[]>>;
};

export function WordInput({ color, i, words, setWords }: WordInputProps) {
  return (
    <input
      type="text"
      name={color + "-" + i}
      className={`${color} rounded-md py-5 text-center font-semibold uppercase placeholder:text-slate-600/50`}
      placeholder="WORD"
      value={words[i]}
      onChange={(e) =>
        setWords((arr) => {
          const newArr = [...arr];
          newArr[i] = e.target.value;
          return newArr;
        })
      }
    />
  );
}
