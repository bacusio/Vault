import { IconPlus } from "@tabler/icons-react";
import Link from "next/link";
import { examples } from "~/lib/examples";

export default function Page() {
  return (
    <main className="flex flex-col gap-4">
      <p>
        This is a simple tool to allow you to create and share your own versions
        of the New York Time&apos;s{" "}
        <a
          href="https://www.nytimes.com/games/connections"
          className="underline"
          target="_blank"
        >
          Connections
        </a>{" "}
        game. All game data is embedded in the URL, so none of your data is
        stored on a server. After generating a new game, just copy the URL to
        send it to your friends. The code for this website is{" "}
        <a
          href="https://github.com/zsrobinson/custom-connections"
          className="underline"
          target="_blank"
        >
          open source
        </a>{" "}
        and was created by{" "}
        <a href="https://zsrobinson.com" className="underline" target="_blank">
          Zachary Robinson
        </a>
        .
      </p>

      <div className="flex flex-col gap-4 sm:flex-row">
        <Link
          href="/new"
          className="flex basis-1/2 flex-col items-center justify-center gap-2 rounded-md border border-stone-300 p-4 transition-colors hover:bg-stone-100"
        >
          <IconPlus className="h-8 w-8 sm:h-12 sm:w-12" />
          <span className="font-semibold">Create New Game</span>
        </Link>

        <div className="flex basis-1/2 flex-col gap-4">
          {examples.map((example, i) => (
            <Link
              href={`/play?options=${btoa(JSON.stringify(example))}`}
              className="rounded-md border border-stone-300 p-4 text-center transition-colors hover:bg-stone-100"
              key={i}
            >
              <span className="font-semibold">
                {example.title.toUpperCase()}
              </span>{" "}
              by {example.author.toUpperCase()}
            </Link>
          ))}
        </div>
      </div>
    </main>
  );
}
