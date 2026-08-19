// "use client";

// import * as React from "react";

// import {
//   Combobox,
//   ComboboxChip,
//   ComboboxChips,
//   ComboboxChipsInput,
//   ComboboxContent,
//   ComboboxEmpty,
//   ComboboxItem,
//   ComboboxList,
//   ComboboxValue,
//   useComboboxAnchor,
// } from "@/components/ui/combobox";

// const frameworks = [
//   "Next.js",
//   "SvelteKit",
//   "Nuxt.js",
//   "Remix",
//   "Astro",
// ] as const;

// export default function ComboboxMultiple() {
//   const anchor = useComboboxAnchor();

//   return (
//     <Combobox
//       multiple
//       autoHighlight
//       items={frameworks}
//       defaultValue={[frameworks[0]]}
//     >
//       <ComboboxChips ref={anchor} className="w-full max-w-xs">
//         <ComboboxValue>
//           {(values) => (
//             <React.Fragment>
//               {values.map((value: string) => (
//                 <ComboboxChip key={value}>{value}</ComboboxChip>
//               ))}
//               <ComboboxChipsInput />
//             </React.Fragment>
//           )}
//         </ComboboxValue>
//       </ComboboxChips>
//       <ComboboxContent anchor={anchor}>
//         <ComboboxEmpty>No items found.</ComboboxEmpty>
//         <ComboboxList>
//           {(item) => (
//             <ComboboxItem key={item} value={item}>
//               {item}
//             </ComboboxItem>
//           )}
//         </ComboboxList>
//       </ComboboxContent>
//     </Combobox>
//   );
// }

"use client";

import React, { useState } from "react";
import {
  Combobox,
  ComboboxChips,
  ComboboxChipsInput,
  ComboboxChip,
  ComboboxValue,
  ComboboxContent,
  ComboboxEmpty,
  ComboboxItem,
  ComboboxList,
  useComboboxAnchor,
} from "./ui/combobox";

type Item = { id: number; name: string };

interface Props {
  inputName: string;
  items: Item[];
  placeholder: string;
  invalidInput?: boolean;
}

export default function MultiSelectBox({
  inputName,
  items,
  placeholder,
  invalidInput = false,
}: Props) {
  const anchor = useComboboxAnchor();
  const [selected, setSelected] = useState<Item[]>([]);

  return (
    <>
      {selected.map((item) => (
        <input
          onChange={() => {}}
          key={item.id}
          type="hidden"
          name={inputName}
          value={item.id}
        />
      ))}
      <Combobox
        items={items}
        onValueChange={(x) => setSelected(x)}
        value={selected}
        multiple
        itemToStringValue={(item: { id: number; name: string }) => item.name}
      >
        <ComboboxChips ref={anchor}>
          <ComboboxValue>
            {(values) => (
              <React.Fragment>
                {values.map((value: Item) => (
                  <ComboboxChip key={value.id}>{value.name}</ComboboxChip>
                ))}
                <ComboboxChipsInput
                  aria-invalid={invalidInput}
                  placeholder={placeholder}
                />
              </React.Fragment>
            )}
          </ComboboxValue>
        </ComboboxChips>
        <ComboboxContent anchor={anchor}>
          <ComboboxEmpty>No items found.</ComboboxEmpty>
          <ComboboxList>
            {(item: Item) => (
              <ComboboxItem key={item.id} value={item}>
                {item.name}
              </ComboboxItem>
            )}
          </ComboboxList>
        </ComboboxContent>
      </Combobox>
    </>
  );
}
