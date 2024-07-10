type FinishedCategoryProps = {
  name: string;
  words: string[];
  color: string;
};

export function FinishedCategory({
  name,
  words,
  color,
}: FinishedCategoryProps) {
  return (
    <div
      className={`${color} flex aspect-[4.25/1] flex-col justify-center rounded-md py-4 text-center uppercase leading-tight sm:aspect-auto`}
    >
      <p className="font-semibold">{name}</p>
      <p>{words.join(", ")}</p>
    </div>
  );
}
