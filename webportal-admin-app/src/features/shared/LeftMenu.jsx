import React, { useEffect, useState } from "react";
import {
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Collapse,
  Divider,
} from "@material-ui/core";

import {
  ShoppingCart,
  PeopleAlt,
  AccountTree,
  BarChart,
  Image,
  Inbox,
  ExpandLess,
  ExpandMore,
  ArrowRight,
  Mail,
  Language,
  Translate,
  Person,
  Security,
  Settings,
  Forum,
  Dashboard,
  FileCopy
} from "@material-ui/icons";
import { useLocation, useHistory } from "react-router-dom";
import useStyles from "./styles";
import { productTypeSelector } from "../../redux/productType/productTypeSlice";
import { getAllAsync } from "../../redux/productType/productTypeAsyncThunk";
import { useDispatch, useSelector } from "react-redux";
import { useTranslation } from "react-i18next";

export default function LeftMenu() {
  let location = useLocation();
  const history = useHistory();
  const classes = useStyles();
  const [openSub, setOpenSub] = useState(false);
  const productType = useSelector(productTypeSelector);
  const dispatch = useDispatch();
  const { t } = useTranslation();

  useEffect(() => {
    if (productType.list.length === 0) {
      dispatch(getAllAsync());
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleClick = () => {
    setOpenSub(!openSub);
  };
  return (
    <List>
      <ListItem
        button
        selected={location.pathname === "/"}
        onClick={() => history.push("/")}
      >
        <ListItemIcon>
          <Dashboard />
        </ListItemIcon>
        <ListItemText primary={t("dashboard")} />
      </ListItem>
      <ListItem
        button
        selected={location.pathname === "/analytics"}
        onClick={() => history.push("/analytics")}
      >
        <ListItemIcon>
          <BarChart />
        </ListItemIcon>
        <ListItemText primary={t("analytics")} />
      </ListItem>
      <Divider />
      <ListItem
        button
        selected={location.pathname === "/category"}
        onClick={() => history.push("/category")}
      >
        <ListItemIcon>
          <AccountTree />
        </ListItemIcon>
        <ListItemText primary={t("category")} />
      </ListItem>
      <ListItem button onClick={handleClick}>
        <ListItemIcon>
          <Inbox />
        </ListItemIcon>
        <ListItemText primary={t("data-type")} />
        {openSub ? <ExpandLess /> : <ExpandMore />}
      </ListItem>
      <Collapse in={openSub} timeout="auto" unmountOnExit>
        <List component="div" disablePadding>
          <ListItem
            button
            selected={location.pathname === "/product-type"}
            className={classes.nested}
            onClick={() => history.push("/product-type")}
          >
            <ListItemIcon>
              <ArrowRight />
            </ListItemIcon>
            <ListItemText
              primary={t("manage-type")}
              className={classes.linkSub}
            />
          </ListItem>
          <Divider />
          {productType.list.map(
            (i) =>
              i.status === "Active" && (
                <ListItem
                  key={i.id}
                  button
                  selected={location.pathname === `/product/${i.code}`}
                  className={classes.nested}
                  onClick={() => history.push(`/product/${i.code}`)}
                >
                  <ListItemIcon>
                    <ArrowRight />
                  </ListItemIcon>
                  <ListItemText primary={i.name} className={classes.linkSub} />
                </ListItem>
              )
          )}
        </List>
      </Collapse>

      <ListItem
        button
        selected={location.pathname === "/banner"}
        onClick={() => history.push("/banner")}
      >
        <ListItemIcon>
          <Image />
        </ListItemIcon>
        <ListItemText primary={t("banner")} />
      </ListItem>
      <ListItem
        button
        selected={location.pathname === "/language"}
        onClick={() => history.push("/language")}
      >
        <ListItemIcon>
          <Language />
        </ListItemIcon>
        <ListItemText primary={t("language")} />
      </ListItem>
      <ListItem
        button
        selected={location.pathname === "/phrase"}
        onClick={() => history.push("/phrase")}
      >
        <ListItemIcon>
          <Translate />
        </ListItemIcon>
        <ListItemText primary={t("phrase")} />
      </ListItem>
      <Divider />
      <ListItem
        button
        selected={location.pathname === "/customer"}
        onClick={() => history.push("/customer")}
      >
        <ListItemIcon>
          <PeopleAlt />
        </ListItemIcon>
        <ListItemText primary={t("customer")} />
      </ListItem>
      <ListItem
        button
        selected={location.pathname === "/order"}
        onClick={() => history.push("/order")}
      >
        <ListItemIcon>
          <ShoppingCart />
        </ListItemIcon>
        <ListItemText primary={t("order")} />
      </ListItem>
      <ListItem
        button
        selected={location.pathname === "/mailbox"}
        onClick={() => history.push("/mailbox")}
      >
        <ListItemIcon>
          <Mail />
        </ListItemIcon>
        <ListItemText primary={t("mail-box")} />
      </ListItem>
      <Divider />

      <ListItem
        button
        selected={location.pathname === "/appuser"}
        onClick={() => history.push("/appuser")}
      >
        <ListItemIcon>
          <Person />
        </ListItemIcon>
        <ListItemText primary={t("user")} />
      </ListItem>
      <ListItem
        button
        selected={location.pathname === "/approle"}
        onClick={() => history.push("/approle")}
      >
        <ListItemIcon>
          <Security />
        </ListItemIcon>
        <ListItemText primary={t("role")} />
      </ListItem>
      <ListItem
        button
        selected={location.pathname === "/configuration"}
        onClick={() => history.push("/configuration")}
      >
        <ListItemIcon>
          <Settings />
        </ListItemIcon>
        <ListItemText primary={t("configuration")} />
      </ListItem>
      <ListItem
        button
        selected={location.pathname === "/chat"}
        onClick={() => history.push("/chat")}
      >
        <ListItemIcon>
          <Forum />
        </ListItemIcon>
        <ListItemText primary={t("chatBox")} />
      </ListItem>
      <ListItem
        button
        selected={location.pathname === "/filemanager"}
        onClick={() => history.push("/filemanager")}
      >
        <ListItemIcon>
          <FileCopy />
        </ListItemIcon>
        <ListItemText primary={t("fileManager")} />
      </ListItem>
    </List>
  );
}
