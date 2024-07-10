import { z } from "zod";

/**
 * The validation schema for the game options object created by `/new` and
 * consumed by `/play`.
 */
export const gameOptionsSchema = z.object({
  words: z.string().array().array(),
  names: z.string().array(),
  author: z.string(), // optional, but just empty if not used
  title: z.string(), // optional, but just empty if not used
});

/**
 * The game options object created by `/new` and consumed by `/play`.
 * @see {@link gameOptionsSchema}
 */
export type GameOptions = z.infer<typeof gameOptionsSchema>;
