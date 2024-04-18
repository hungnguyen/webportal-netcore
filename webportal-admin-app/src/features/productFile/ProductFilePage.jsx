import React, { useEffect } from "react";
import { Switch, Route } from "react-router-dom";
import { Typography } from "@material-ui/core";
import Edit from "./Edit";
import List from "./List";
import { productSelector, select } from "../../redux/product/productSlice";
import { useParams } from "react-router-dom";
import { useSelector, useDispatch } from "react-redux";
import { useTranslation } from "react-i18next";

export default function ProductFilePage() {
  const { productid } = useParams();
  const product = useSelector(productSelector);
  const dispatch = useDispatch();
  const { t } = useTranslation();

  useEffect(() => {
    dispatch(select(productid));
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [productid]);

  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("product-file")}
      </Typography>
      <Typography variant="subtitle1" gutterBottom>
        {product.item.name}
      </Typography>

      <Switch>
        <Route exact path="/product-file/:type/:productid" component={List} />
        <Route
          path="/product-file/:type/:productid/edit/:id?"
          component={Edit}
        />
      </Switch>
    </>
  );
}
