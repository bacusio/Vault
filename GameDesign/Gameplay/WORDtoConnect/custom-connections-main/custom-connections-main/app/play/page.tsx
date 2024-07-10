"use client";

import { useAutoAnimate } from "@formkit/auto-animate/react";
import Link from "next/link";
import { useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";
import toast, { Toaster } from "react-hot-toast";
import { CircularButton } from "~/components/circular-button";
import {
  colors,
  getColor,
  getCommonColors,
  getTotalMistakes,
  regenerateWordPool,
  validateGuess,
} from "~/lib/game";
import { GameOptions, gameOptionsSchema } from "~/lib/game-options";
import { alphabetical, hasSameElements, range, toSwapped } from "~/lib/utils";
import { FinishedCategory } from "./finished-category";
import { WordTile } from "./word-tile";

/* eslint-disable react-hooks/exhaustive-deps */

export default function Page() {
  const params = useSearchParams();
  const [mistakesAnimateRef] = useAutoAnimate();
  const [poolAnimateRef, setPoolAnimated] = useAutoAnimate();

  const [gameOptions, setGameOptions] = useState<GameOptions | undefined>();
  const [selectedWords, setSelectedWords] = useState<string[]>([]);
  const [wordPool, setWordPool] = useState<string[]>([]);
  const [guesses, setGuesses] = useState<string[][]>([]);

  useEffect(() => {
    if (!params.has("options")) return;

    try {
      const decoded = JSON.parse(atob(params.get("options")!));
      const options = gameOptionsSchema.parse(decoded);
      console.info("setting game options from URL", options);

      for (let i = 0; i < 4; i++) {
        options.words[i].sort(alphabetical);
      }

      console.info("setting game options from URL", options);

      setGameOptions(options);
      setWordPool(regenerateWordPool(options, guesses));
    } catch {
      console.error("could not parse game options from URL");
    }
  }, []);

  // ensure that gameOptions won't be undefined
  if (gameOptions === undefined) {
    return <main></main>;
  }

  // derived state
  const totalMistakes = getTotalMistakes(gameOptions, guesses);
  const remainingMistakes = 4 - totalMistakes;
  const wonGame = wordPool.length === 0;
  const lostGame = remainingMistakes === 0;
  const submitDisabled =
    selectedWords.length !== 4 ||
    guesses.some((guess) => hasSameElements(guess, selectedWords));

  return (
    <main className="flex flex-col gap-4">
      <div>
        <Toaster
          containerStyle={{
            position: "relative",
            inset: 0,
            flexShrink: 0,
          }}
          toastOptions={{
            duration: 3000,
            className:
              "!shrink-0 !rounded-md !bg-stone-900 !p-2 !text-stone-50 !shadow-md",
          }}
        />

        <h2>
          <span className="text-2xl font-semibold">
            {gameOptions.title.toUpperCase()}
          </span>{" "}
          by {gameOptions.author.toUpperCase()}
        </h2>

        <p className="text-stone-500">
          <Link
            href={`/new?options=${params.get("options")}`}
            className="underline"
          >
            remix this game
          </Link>{" "}
          or{" "}
          <Link href="/new" className="underline">
            create your own
          </Link>
        </p>
      </div>

      <div className="flex flex-col gap-2 sm:gap-4">
        {/* print out finished categories */}
        {guesses.map((guess) =>
          validateGuess(gameOptions, guess) ? (
            <FinishedCategory
              name={gameOptions.names[getColor(gameOptions, guess[0])]}
              words={gameOptions.words[getColor(gameOptions, guess[0])]}
              color={colors[getColor(gameOptions, guess[0])]}
              key={getColor(gameOptions, guess[0])}
            />
          ) : undefined,
        )}

        {/* grid from the word pool */}
        {wordPool.length > 0 && (
          <div className="grid grid-cols-4 gap-2 sm:gap-4" ref={poolAnimateRef}>
            {wordPool.map((word) => (
              <WordTile
                key={word}
                selected={selectedWords.includes(word)}
                onClick={() => {
                  if (selectedWords.includes(word)) {
                    setSelectedWords(selectedWords.filter((w) => w !== word));
                  } else if (selectedWords.length < 4) {
                    setSelectedWords([...selectedWords, word]);
                  }
                }}
              >
                {word}
              </WordTile>
            ))}
          </div>
        )}
      </div>

      {/* display the mistakes that the user has left */}
      <div
        className="flex items-center gap-2 place-self-center sm:place-self-auto"
        ref={mistakesAnimateRef}
      >
        <p>Remaining mistakes:</p>
        {range(remainingMistakes).map((_, i) => (
          <span className="h-4 w-4 rounded-full bg-stone-600" key={i}></span>
        ))}
      </div>

      {!wonGame && !lostGame ? (
        <div className="flex flex-wrap gap-4 place-self-center sm:place-self-auto">
          <CircularButton
            onClick={() =>
              setWordPool(regenerateWordPool(gameOptions, guesses))
            }
          >
            Shuffle
          </CircularButton>

          <CircularButton
            disabled={selectedWords.length === 0}
            onClick={() => setSelectedWords([])}
          >
            Deselect all
          </CircularButton>

          <CircularButton
            variant={submitDisabled ? undefined : "filled"}
            disabled={submitDisabled}
            onClick={async () => {
              const result = validateGuess(gameOptions, selectedWords);

              if (result === true) {
                // sort so the words swap to the right order
                const sortedSelected = selectedWords.toSorted(alphabetical);

                // swap guesses into the beginning of the pool
                for (let a = 0; a < 4; a++) {
                  setWordPool((pool) => {
                    const b = pool.indexOf(sortedSelected[a]);
                    return toSwapped(pool, a, b);
                  });
                }

                setPoolAnimated(true);

                await new Promise((res) => setTimeout(res, 500));
                setGuesses([...guesses, selectedWords]);
                setSelectedWords([]);
                setWordPool((pool) =>
                  pool.filter((word) => !selectedWords.includes(word)),
                );

                setPoolAnimated(false);
              } else {
                if (getCommonColors(gameOptions, selectedWords) === 3) {
                  toast("One away...");
                }

                setGuesses([...guesses, selectedWords]);
                setSelectedWords([]);
              }
            }}
          >
            Submit
          </CircularButton>
        </div>
      ) : wonGame ? (
        <p>You won, congrats!</p>
      ) : lostGame ? (
        <p>You lost, refresh to try again!</p>
      ) : null}
    </main>
  );
}
