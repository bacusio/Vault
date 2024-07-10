import { Dispatch, SetStateAction } from "react";
import { range } from "~/lib/utils";
import { CategoryNameInput } from "./category-name-input";
import { WordInput } from "./word-input";

type CategoryInputProps = {
  color: string;
  name: string;
  setName: Dispatch<SetStateAction<string>>;
  words: string[];
  setWords: Dispatch<SetStateAction<string[]>>;
};

export function CategoryInput({
  color,
  name,
  setName,
  words,
  setWords,
}: CategoryInputProps) {
  return (
    <>
      <CategoryNameInput color={color} name={name} setName={setName} />
      <div className="grid grid-cols-2 gap-2 sm:grid-cols-4 sm:gap-4">
        {range(4).map((i) => (
          <WordInput
            color={color}
            i={i}
            key={i}
            words={words}
            setWords={setWords}
          />
        ))}
      </div>
    </>
  );
}
