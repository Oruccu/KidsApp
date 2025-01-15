import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { createNativeStackNavigator } from "@react-navigation/native-stack";

import Home from "@/app/page/home";
import Settings from "@/app/page/settings";
import Story from "@/app/page/story";

import ChildSignUp from "@/app/page/auth/childSignUp";

import KidsMode from "@/app/page/kidsMode";
import SignIn from "@/app/page/auth/signIn";
import ResetPassword from "@/app/page/auth/resetPassword";

import store from "./store";
import { Provider } from "react-redux";

const Tab = createBottomTabNavigator();
const Stack = createNativeStackNavigator();

function MainTab() {
  return (
    <Tab.Navigator screenOptions={{ headerShown: false }}>
      <Tab.Screen name="Home" component={Home} />
      <Tab.Screen name="Story" component={Story} />
      <Tab.Screen name="Settings" component={Settings} />
    </Tab.Navigator>
  );
}
function AuthStack() {
  return (
    <Stack.Navigator screenOptions={{ headerShown: false }}>
      <Stack.Screen name="SignIn" component={SignIn} />
      <Stack.Screen name="ChildSignUp" component={ChildSignUp} />
      <Stack.Screen name="ResetPassword" component={ResetPassword} />
      <Stack.Screen name="KidsMode" component={KidsMode} />
    </Stack.Navigator>
  );
}

export default function Index() {
  return (
    <Provider store={store}>
      <Stack.Navigator screenOptions={{ headerShown: false }}>
        <Stack.Screen name="Auth" component={AuthStack} />
        <Stack.Screen name="Main" component={MainTab} />
      </Stack.Navigator>
    </Provider>
  );
}
