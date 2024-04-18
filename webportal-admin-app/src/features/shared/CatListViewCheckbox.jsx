import React, { useEffect, useState } from "react";
import CatListItemCheckbox from "./CatListItemCheckbox";

const CatListViewCheckbox = ({
  all,
  parentid,
  className,
  selectedCats,
  onChange,
}) => {
  const [list, setList] = useState([]);

  useEffect(() => {
    setList(all.filter((i) => i.parentid === parentid));
  }, [all, parentid]);

  return (
    <>
      {list.map((i) => {
        return (
          <CatListItemCheckbox
            all={all}
            item={i}
            className={className}
            selectedCats={selectedCats}
            onChange={onChange}
            key={i.id}
          />
        );
      })}
    </>
  );
};

export default CatListViewCheckbox;
