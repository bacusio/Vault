import { ComponentPropsWithoutRef } from "react";

type CircularButtonProps = ComponentPropsWithoutRef<"button"> & {
  variant?: "filled";
};

export function CircularButton({
  disabled,
  variant,
  ...props
}: CircularButtonProps) {
  return (
    <button
      {...props}
      className={`rounded-full border border-black px-4 py-2
          ${disabled ? "opacity-40" : ""}
          ${variant === "filled" ? "bg-black text-white" : ""}`}
      disabled={disabled}
    />
  );
}
