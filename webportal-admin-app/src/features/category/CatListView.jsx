import React, { useEffect, useState } from "react";
import CatListItem from "./CatListItem";

const CatListView = ({ all, parentid, className }) => {
  const [list, setList] = useState([]);

  useEffect(() => {
    setList(all.filter((i) => i.parentid === parentid));
  }, [all, parentid]);

  return (
    <>
      {list.map((i) => {
        return (
          <CatListItem all={all} item={i} className={className} key={i.id} />
        );
      })}
    </>
  );
};

export default CatListView;
