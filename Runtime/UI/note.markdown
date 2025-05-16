# UI Element Hierarchy

## Layer Order (from bottom to top)
1. **View** (Base Layer)
   - Main UI elements
   - Always visible
   - Layer index: 0

2. **Popup** (Middle Layer)
   - Modal windows
   - Auto-hides previous popup when new one opens
   - Layer index: 1

3. **Dialog** (Top Layer)
   - Critical notifications
   - Always appears above popups
   - Layer index: 2

## Behavior Notes
- Only one popup can be visible at a time
- Dialogs can stack on top of popups
- Views remain visible unless explicitly hidden