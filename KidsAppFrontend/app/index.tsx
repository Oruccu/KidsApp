import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { createNativeStackNavigator } from "@react-navigation/native-stack";

import Home from "@/app/page/home";
import Settings from "@/app/page/settings";
import Story from "@/app/page/story";

import ChildSingUp from "@/app/page/auth/childSingUp";
import ParentSingUp from "@/app/page/auth/parentSingUp";
import KidsMode from "@/app/page/kidsMode";
import SingIn from "@/app/page/auth/singIn";
import ResetPassword from "@/app/page/auth/resetPassword";

const Tab = createBottomTabNavigator();
const Stack = createNativeStackNavigator();

function MainTab() {
  return (
    <Tab.Navigator>
      <Tab.Screen name="Home" component={Home} />
      <Tab.Screen name="Story" component={Story} />
      <Tab.Screen name="Settings" component={Settings} />
    </Tab.Navigator>
  );
}
  function AuthStack() {
    return (
      <Stack.Navigator screenOptions={{headerShown:false}}>
        <Stack.Screen name="ParentSingUp" component={ParentSingUp} />
        <Stack.Screen name="ChildSingUp" component={ChildSingUp} />
        <Stack.Screen name="SingIn" component={SingIn} />
        <Stack.Screen name="ResetPassword" component={ResetPassword} />
        <Stack.Screen name="KidsMode" component={KidsMode} />
      </Stack.Navigator>
    );
  }


export default function Index() 
{
  return (
    <Stack.Navigator screenOptions={{headerShown:false}}>
      <Stack.Screen name="Auth" component={AuthStack} />
      <Stack.Screen name="Main" component={MainTab} />
    </Stack.Navigator>
  );
}
