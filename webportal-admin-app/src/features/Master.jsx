import React, { useEffect } from "react";
import { makeStyles } from "@material-ui/core/styles";
import {
  Drawer,
  AppBar,
  CssBaseline,
  Toolbar,
  Typography,
  IconButton,
  Menu,
  MenuItem,
  Avatar,
  Switch as SwitchButton,
} from "@material-ui/core";

import { AccessTime, AccountCircle, Error, Today } from "@material-ui/icons";
import { Switch, Route, NavLink, useHistory } from "react-router-dom";

import ProductTypePage from "./productType/ProductTypePage";
import PhrasePage from "./phrase/PhrasePage";
import LanguagePage from "./language/LanguagePage";
import BannerPage from "./banner/BannerPage";
import CustomerPage from "./customer/CustomerPage";
import MailBoxPage from "./mailbox/MailBoxPage";
import CategoryPage from "./category/CategoryPage";
import OrderPage from "./order/OrderPage";
import ProductPage from "./product/ProductPage";
import ProductFilePage from "./productFile/ProductFilePage";
import AppUserPage from "./appuser/AppUserPage";
import AppRolePage from "./approle/AppRolePage";
import WebsitePage from "./website/WebsitePage";
import ProfilePage from "./home/ProfilePage";

import Notification from "./shared/Notification";
import Dashboard from "./home/Dashboard";
import {
  getProfileAsync,
} from "../redux/account/accountAsyncThunk";
import * as enumsAsyncThunk from "../redux/enum/enumAsyncThunk";
import { useDispatch, useSelector } from "react-redux";
import { accountSelector } from "../redux/account/accountSlice";
import { applicationSelector } from "../redux/application/applicationSlice";
import { getByDomainAsync } from "../redux/website/websiteAsyncThunk";

import LeftMenu from "./shared/LeftMenu";
import Moment from "react-moment";
import { useTranslation } from "react-i18next";
import ChatPage from "./chat/ChatPage";
import Analytics from "./home/Analytics";
import LogPage from "./loginfo/LogPage";
import FileManagerPage from "./fileManager/FileManagerPage"

const drawerWidth = 240;

const useStyles = makeStyles((theme) => ({
  root: {
    display: "flex",
  },
  appBar: {
    zIndex: theme.zIndex.drawer + 1,
  },
  drawer: {
    width: drawerWidth,
    flexShrink: 0,
  },
  drawerPaper: {
    width: drawerWidth,
  },
  drawerContainer: {
    overflow: "auto",
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing(3),
  },
  title: {
    flexGrow: 1,
  },
  link: {
    color: "inherit",
    textDecoration: "none",
  },
}));

function Master() {
  const classes = useStyles();
  const dispatch = useDispatch();
  const account = useSelector(accountSelector);
  const application = useSelector(applicationSelector);
  const history = useHistory();

  const [anchorEl, setAnchorEl] = React.useState(null);
  const open = Boolean(anchorEl);
  const { i18n, t } = useTranslation();
  const [lang, setLang] = React.useState("en");

  const handleChange = (e) => {
    let selected = lang === "en" ? "vi" : "en";
    setLang(selected);
    i18n.changeLanguage(selected);
  };

  useEffect(() => {
    if (!application.ischeck) {
      let domain = window.location.hostname;
      dispatch(getByDomainAsync(domain));
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    dispatch(enumsAsyncThunk.getGenderAsync());
    dispatch(enumsAsyncThunk.getOrderStatusAsync());
    dispatch(enumsAsyncThunk.getPayMethodAsync());
    dispatch(enumsAsyncThunk.getPayStatusAsync());
    dispatch(enumsAsyncThunk.getPositionAsync());
    dispatch(enumsAsyncThunk.getStatusAsync());
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (account.profile.username === "") {
      dispatch(getProfileAsync());
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleMenu = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };
  
  return (
    <div className={classes.root}>
      <CssBaseline />
      <AppBar position="fixed" className={classes.appBar}>
        <Toolbar>
          <Typography variant="h6" noWrap className={classes.title}>
            {t("admin-page")}
          </Typography>
          <div>
            <IconButton color="inherit" onClick={() => history.push("/loginfo")}>
              <Error />
            </IconButton>
            {lang.toUpperCase()}
            <SwitchButton
              checked={lang === "en"}
              onChange={handleChange}
              color="default"
              name="changeLang"
              inputProps={{ "aria-label": "default checkbox" }}
            />
            <IconButton color="inherit">
              <Today />
            </IconButton>
            <Moment format="dddd, D/M/YYYY" local></Moment>
            <IconButton color="inherit">
              <AccessTime />
            </IconButton>
            <Moment format="HH:mm:ss" interval={1000} local></Moment>
            <IconButton
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleMenu}
              color="inherit"
            >
              {account.profile.image !== "" ? (
                <Avatar
                  alt={account.profile.fullname ?? account.profile.username}
                  src={`${application.imageBaseAddress}/${account.profile.image}`}
                />
              ) : (
                <AccountCircle />
              )}
            </IconButton>
            <Menu
              id="menu-appbar"
              anchorEl={anchorEl}
              anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
              transformOrigin={{ vertical: "top", horizontal: "center" }}
              getContentAnchorEl={null}
              keepMounted
              open={open}
              onClose={handleClose}
            >
              <MenuItem>
                {account.profile.fullname ?? account.profile.username}
              </MenuItem>
              <MenuItem>
                <NavLink className={classes.link} to="/profile">
                  {t("profile")}
                </NavLink>
              </MenuItem>
              <MenuItem>
                <a href="/login" className={classes.link}>
                  Logout
                </a>
              </MenuItem>
              {/* onClick={doLogout} */}
            </Menu>
          </div>
        </Toolbar>
      </AppBar>
      <Drawer
        className={classes.drawer}
        variant="permanent"
        classes={{
          paper: classes.drawerPaper,
        }}
      >
        <Toolbar />
        <div className={classes.drawerContainer}>
          <LeftMenu />
        </div>
      </Drawer>
      <main className={classes.content}>
        <Toolbar />
        <Notification />
        <Switch>
          <Route exact path="/" component={Dashboard} />
          <Route path="/analytics" component={Analytics} />
          <Route path="/product-type" component={ProductTypePage} />
          <Route path="/phrase" component={PhrasePage} />
          <Route path="/language" component={LanguagePage} />
          <Route path="/banner" component={BannerPage} />
          <Route path="/customer" component={CustomerPage} />
          <Route path="/mailbox" component={MailBoxPage} />
          <Route path="/category" component={CategoryPage} />
          <Route path="/order" component={OrderPage} />
          <Route path="/product" component={ProductPage} />
          <Route path="/appuser" component={AppUserPage} />
          <Route path="/approle" component={AppRolePage} />
          <Route path="/configuration" component={WebsitePage} />
          <Route path="/profile" component={ProfilePage} />
          <Route path="/chat" component={ChatPage} />
          <Route path="/loginfo" component={LogPage} />
          <Route path="/fileManager" component={FileManagerPage} />
          <Route
            path="/product-file/:type/:productid"
            component={ProductFilePage}
          />
        </Switch>
      </main>
    </div>
  );
}

export default Master;
