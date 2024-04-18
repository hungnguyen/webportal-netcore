import { configureStore } from "@reduxjs/toolkit";
import accountReducer from "../redux/account/accountSlice";
import noticeReducer from "../redux/notice/noticeSlice";
import productTypeReducer from "../redux/productType/productTypeSlice";
import enumReducer from "../redux/enum/enumSlice";
import phraseReducer from "../redux/phrase/phraseSlice";
import languageReducer from "../redux/language/languageSlice";
import bannerReducer from "../redux/banner/bannerSlice";
import customerReducer from "../redux/customer/customerSlice";
import mailboxReducer from "../redux/mailbox/mailboxSlice";
import categoryReducer from "../redux/category/categorySlice";
import orderReducer from "../redux/order/orderSlice";
import appuserReducer from "../redux/appuser/appuserSlice";
import productReducer from "../redux/product/productSlice";
import productInCategoryReducer from "../redux/productInCategory/productInCategorySlice";
import productFileReducer from "../redux/productFile/productFileSlice";
import approleReducer from "../redux/approle/approleSlice";
import appuserroleReducer from "../redux/appuserrole/appuserroleSlice";
import websiteReducer from "../redux/website/websiteSlice";
import applicationReducer from "../redux/application/applicationSlice";
import chatReducer from "../redux/chat/chatSlice";
import analyticsReducer from "../redux/analytics/analyticsSlice";
import logReducer from "../redux/loginfo/logSlice";
import folderReducer from "../redux/folder/folderSlice";
import fileReducer from "../redux/file/fileSlice";

export const store = configureStore({
  reducer: {
    account: accountReducer,
    notice: noticeReducer,
    productType: productTypeReducer,
    enums: enumReducer,
    phrase: phraseReducer,
    language: languageReducer,
    banner: bannerReducer,
    customer: customerReducer,
    mailbox: mailboxReducer,
    category: categoryReducer,
    order: orderReducer,
    appuser: appuserReducer,
    product: productReducer,
    productInCategory: productInCategoryReducer,
    productFile: productFileReducer,
    approle: approleReducer,
    appuserrole: appuserroleReducer,
    website: websiteReducer,
    application: applicationReducer,
    chat: chatReducer,
    analytics: analyticsReducer,
    log: logReducer,
    folder: folderReducer,
    file: fileReducer
  },
});
