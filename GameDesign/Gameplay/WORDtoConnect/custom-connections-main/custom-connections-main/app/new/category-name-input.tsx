import { Dispatch, SetStateAction } from "react";

type CategoryNameInputProps = {
  color: string;
  name: string;
  setName: Dispatch<SetStateAction<string>>;
};

export function CategoryNameInput({
  color,
  name,
  setName,
}: CategoryNameInputProps) {
  return (
    <div className="flex items-center gap-2">
      <div className={`${color} h-6 w-6 rounded-full`}></div>
      <input
        type="text"
        placeholder="CATEGORY NAME"
        className="min-w-[304px] border-b border-black bg-stone-50 px-1 font-semibold uppercase placeholder-stone-400"
        value={name}
        onChange={(e) => setName(e.target.value)}
      />
    </div>
  );
}
