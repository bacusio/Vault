import { Dispatch, ReactNode, SetStateAction } from "react";

type TextInputProps = {
  text: string;
  setText: Dispatch<SetStateAction<string>>;
  name: string;
  icon: ReactNode;
};

export function TextInput({ text, setText, name, icon }: TextInputProps) {
  return (
    <div className="flex items-center gap-2">
      {icon}
      <input
        type="text"
        placeholder={name}
        className="min-w-[304px] border-b border-stone-900 bg-stone-50 px-1 font-semibold uppercase placeholder-stone-400"
        value={text}
        onChange={(e) => setText(e.target.value)}
      />
    </div>
  );
}
