# selector

## & nesting selector

```css
.parent-rule {
  /* parent rule properties */
  &:hover {
    /* child rule properties */
  }
  .featured & {
    /* .featured .card styles */
  }
}

/* the browser parses this as */
.parent-rule {
  /* parent rule properties */
}

.parent-rule:hover {
  /* child rule properties */
}

.featured .parent-rule {
  /* .featured .card styles */
}
```
