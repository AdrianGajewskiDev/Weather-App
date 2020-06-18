import {
  animation,
  trigger,
  transition,
  animate,
  style,
  query,
  state,
} from "@angular/animations";

export const slideAnimation = trigger("slideInOut", [
  state(
    "slideIn",
    style({
      transform: "translate(0)",
    })
  ),
  state(
    "slideOut",
    style({
      transform: "translate(-300%)",
    })
  ),
  transition("* => slideIn", animate("0.3s")),
  transition("* => slideOut", animate("0.3s")),
]);
