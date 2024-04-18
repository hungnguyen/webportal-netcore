import { makeStyles, alpha } from "@material-ui/core/styles";
import { green, blue, red, orange, grey } from "@material-ui/core/colors";

const useStyles = makeStyles((theme) => ({
  "@global": {
    "*::-webkit-scrollbar": {
      width: "0.4em",
    },
    "*::-webkit-scrollbar-track": {
      "-webkit-box-shadow": "inset 0 0 6px rgba(0,0,0,0.00)",
    },
    "*::-webkit-scrollbar-thumb": {
      backgroundColor: "rgba(0,0,0,.1)",
      outline: "none",
    },
  },
  root: {
    display: "flex",
  },
  appBar: {
    zIndex: theme.zIndex.drawer + 1,
  },
  drawer: {
    width: 240,
    flexShrink: 0,
  },
  drawerPaper: {
    width: 240,
  },
  drawerContainer: {
    overflow: "auto",
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing(3),
  },
  link: {
    color: "inherit",
    textDecoration: "none",
  },
  tablePaper: {
    marginTop: theme.spacing(2),
  },
  toolbar: {
    flexGrow: 1,
  },
  toolbarDialog: {
    flexGrow: 1,
    padding: 0,
  },
  search: {
    position: "relative",
    borderRadius: theme.shape.borderRadius,
    backgroundColor: alpha(theme.palette.common.white, 0.15),
    "&:hover": {
      backgroundColor: alpha(theme.palette.common.white, 0.25),
    },
    marginLeft: 0,
    width: "100%",
    [theme.breakpoints.up("sm")]: {
      marginLeft: theme.spacing(1),
      width: "auto",
    },
  },
  searchIcon: {
    padding: theme.spacing(0, 2),
    height: "100%",
    position: "absolute",
    pointerEvents: "none",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
  },
  inputRoot: {
    color: "inherit",
    border: "none",
  },
  inputInput: {
    padding: theme.spacing(1, 1, 1, 0),
    // vertical padding + font size from searchIcon
    paddingLeft: `calc(1em + ${theme.spacing(4)}px)`,
    transition: theme.transitions.create("width"),
    width: "100%",
    border: "solid 1px #e0e0e0",
    borderRadius: theme.shape.borderRadius,
    [theme.breakpoints.up("sm")]: {
      width: "12ch",
      "&:focus": {
        width: "20ch",
      },
    },
  },
  title: {
    flexGrow: 1,
    display: "none",
    [theme.breakpoints.up("sm")]: {
      display: "block",
    },
  },
  form: {
    "& .MuiTextField-root": {
      width: "100%",
    },
    "& .MuiFormControl-root": {
      width: "100%",
    },
  },
  formDialog: {
    "& .MuiTextField-root": {
      marginTop: theme.spacing(1),
      marginBottom: theme.spacing(1),
      width: "100%",
    },
    "& .MuiFormControl-root": {
      marginTop: theme.spacing(1),
      marginBottom: theme.spacing(1),
      width: "100%",
    },
    "& .MuiFormControlLabel-root": {
      marginTop: theme.spacing(1),
      marginBottom: theme.spacing(1),
    },
  },
  saveButton: {
    marginRight: theme.spacing(2),
  },
  successButton: {
    color: theme.palette.success.main,
  },
  formNavigation: {
    padding: theme.spacing(2),
  },
  formSection: {
    marginTop: theme.spacing(1),
    marginBottom: theme.spacing(1),
  },
  columnSpacing: {
    "& .MuiGrid-root": {
      marginTop: theme.spacing(3),
      marginBottom: theme.spacing(3),
    },
  },
  nested: {
    paddingLeft: theme.spacing(4),
  },
  nested6: {
    paddingLeft: theme.spacing(6),
  },
  linkSub: {
    "& .MuiTypography-root": {
      fontSize: "14px",
    },
  },
  hidden: {
    display: "none",
  },
  image: {
    maxWidth: "100%",
  },
  height400: {
    height: "400px",
    overflow: "auto",
  },
  avatarRed: {
    color: "#fff",
    backgroundColor: red[500],
  },
  avatarGreen: {
    color: "#fff",
    backgroundColor: green[500],
  },
  avatarBlue: {
    color: "#fff",
    backgroundColor: blue[500],
  },
  avatarOrange: {
    color: "#fff",
    backgroundColor: orange[500],
  },
  colorRed: {
    color: red[500],
  },
  colorBlue: {
    color: blue[500],
  },
  colorGreen: {
    color: green[500],
  },
  colorOrange: {
    color: orange[500],
  },
  messageBox: {
    border: "solid 1px #e0e0e0",
    borderRadius: theme.shape.borderRadius,
    width: "100%",
    height: `calc(100vh - 300px)`,
    marginBottom: theme.spacing(2),
    overflowY: "scroll",
    padding: theme.spacing(2),
  },
  messageIn: {
    borderRadius: "10px",
    backgroundColor: grey[100],
    maxWidth: "40%",
    padding: theme.spacing(2),
    marginBottom: "1px",
    float: "left",
  },
  messageOut: {
    borderRadius: "10px",
    backgroundColor: blue[100],
    maxWidth: "40%",
    padding: theme.spacing(2),
    float: "right",
    marginBottom: "1px",
  },
  topListTable: {
    height: "220px",
    overflowY: "auto",
    padding: theme.spacing(1),
  },
  saleChart: {
    padding: theme.spacing(1),
    height: "420px",
  },
  browserChart: {
    padding: theme.spacing(1),
    height: "420px",
  },
  paper: {
    padding: theme.spacing(2),
    textAlign: 'center',
    color: theme.palette.text.secondary,
    border: "solid 1px",
    borderColor: "#fff",
    "&:hover":{
      borderColor: grey[200]
    },
    "&.selected":{
      backgroundColor: blue[100]
    },
    "&.folder .MuiSvgIcon-root": {
      fontSize: "40px",
      color: theme.palette.info.main
    }
  },
}));

export default useStyles;
