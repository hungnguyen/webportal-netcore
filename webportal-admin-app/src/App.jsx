import React, { useEffect } from "react";
import Master from "./features/Master";

import { createTheme, ThemeProvider } from "@material-ui/core/styles";
import { useDispatch } from "react-redux";
import { checkLoginAsync } from "./redux/account/accountAsyncThunk";

const theme = createTheme({
  palette: {
    primary: {
      main: "#1976d2",
    },
  },
});

function App() {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(checkLoginAsync());
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
    <ThemeProvider theme={theme}>
      {/* {account.checking && !account.login ? (
        <></>
      ) : account.login ? ( */}
      <Master />
      {/* ) : (
        <Login />
      )} */}
    </ThemeProvider>
  );
}

export default App;
