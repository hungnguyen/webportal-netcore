import React, { useEffect, useState } from "react";
import { Switch, Route, useLocation } from "react-router-dom";
import { Typography } from "@material-ui/core";
import Edit from "./Edit";
import List from "./List";
import { productTypeSelector } from "../../redux/productType/productTypeSlice";
import { useSelector } from "react-redux";

export default function ProductPage() {
  const productType = useSelector(productTypeSelector);
  const location = useLocation();
  const [currentType, setCurrentType] = useState({ name: "" });

  useEffect(() => {
    let type = location.pathname.split("/")[2];
    setCurrentType(productType.list.find((i) => i.code === type));
  }, [location, productType.list]);

  return (
    <>
      <Typography variant="h4" gutterBottom>
        {currentType?.name}
      </Typography>
      <Switch>
        <Route exact path="/product/:type" component={List} />
        <Route path="/product/:type/edit/:id?" component={Edit} />
      </Switch>
    </>
  );
}
