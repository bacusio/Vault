"use client";

import { IconTextSize, IconUser } from "@tabler/icons-react";
import { useRouter, useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";
import { CircularButton } from "~/components/circular-button";
import { colors } from "~/lib/game";
import { GameOptions, gameOptionsSchema } from "~/lib/game-options";
import { range } from "~/lib/utils";
import { CategoryInput } from "./category-input";
import { TextInput } from "./text-input";

/* eslint-disable react-hooks/exhaustive-deps */

export default function Page() {
  const router = useRouter();
  const params = useSearchParams();

  const [yellowWords, setYellowWords] = useState(["", "", "", ""]);
  const [greenWords, setGreenWords] = useState(["", "", "", ""]);
  const [blueWords, setBlueWords] = useState(["", "", "", ""]);
  const [purpleWords, setPurpleWords] = useState(["", "", "", ""]);

  // prettier-ignore
  const setWords = [setYellowWords, setGreenWords, setBlueWords, setPurpleWords];
  const words = [yellowWords, greenWords, blueWords, purpleWords];

  const [yellowName, setYellowName] = useState("");
  const [greenName, setGreenName] = useState("");
  const [blueName, setBlueName] = useState("");
  const [purpleName, setPurpleName] = useState("");

  const setNames = [setYellowName, setGreenName, setBlueName, setPurpleName];
  const names = [yellowName, greenName, blueName, purpleName];

  const [title, setTitle] = useState("");
  const [author, setAuthor] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    if (!params.has("options")) return;

    try {
      const decoded = JSON.parse(atob(params.get("options")!));
      const options = gameOptionsSchema.parse(decoded);
      console.log("setting game options from URL", options);

      setNames.forEach((set, i) => set(options.names[i]));
      setWords.forEach((set, i) => set(options.words[i]));

      setTitle(options.title);
      setAuthor(options.author);
    } catch {
      console.error("could not parse game options from URL");
    }
  }, [params]);

  return (
    <main className="flex flex-col gap-4">
      <div className="flex flex-wrap gap-4">
        <TextInput
          name="title (optional)"
          icon={<IconTextSize />}
          text={title}
          setText={setTitle}
        />

        <TextInput
          name="author (optional)"
          icon={<IconUser />}
          text={author}
          setText={setAuthor}
        />
      </div>

      {range(4).map((i) => (
        <CategoryInput
          key={i}
          color={colors[i]}
          name={names[i]}
          setName={setNames[i]}
          words={words[i]}
          setWords={setWords[i]}
        />
      ))}

      <div className="flex flex-wrap items-center gap-4">
        <CircularButton
          onClick={() => {
            setNames.forEach((set) => set(""));
            setWords.forEach((set) => set(["", "", "", ""]));

            setTitle("");
            setAuthor("");
            setError("");
          }}
        >
          Clear all
        </CircularButton>

        <CircularButton
          variant="filled"
          onClick={() => {
            // create object and clean all inputs
            const obj: GameOptions = {
              names: names.map((word) => word.trim().toLowerCase()),
              words: words.map((arr) =>
                arr.map((word) => word.trim().toLowerCase()),
              ),
              author: author.trim().toLowerCase(),
              title: title.trim().toLowerCase(),
            };

            // validate that all fields are filled
            if (!obj.words.flat().every((s) => s.length > 0)) {
              setError("Please enter all words.");
              return;
            } else if (!obj.names.every((s) => s.length > 0)) {
              setError("Please enter all category names.");
              return;
            }

            // validate that there aren't any duplicate words
            const wordSet = new Set<string>();
            words.flat().forEach((word) => wordSet.add(word));
            if (wordSet.size !== 16) {
              setError("Please remove any duplicate words.");
              return;
            }

            // validate that there aren't any duplicate category names
            const nameSet = new Set<string>();
            names.forEach((name) => nameSet.add(name));
            if (nameSet.size !== 4) {
              setError("Please remove any duplicate category names.");
              return;
            }

            const encoded = btoa(JSON.stringify(obj));
            router.push(`/play?options=${encoded}`);
          }}
        >
          Submit
        </CircularButton>

        {error && <p className="text-red-600">{error}</p>}
      </div>
    </main>
  );
}
