import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { useSelector } from 'react-redux';
import { Image, StyleSheet } from 'react-native';
import { RootState } from './store/store';

import Home from "@/app/page/home";
import Settings from "@/app/page/settings";
import Story from "@/app/page/story";

import ChildSignUp from "@/app/page/auth/childSignUp";
import KidsMode from "@/app/page/kidsMode";
import SignIn from "@/app/page/auth/signIn";
import ResetPassword from "@/app/page/auth/resetPassword";
import Game from "@/app/page/game";
import store from "./store/store";
import { Provider } from "react-redux";
import color from "./styles/color";

const Tab = createBottomTabNavigator();
const Stack = createNativeStackNavigator();

function MainTab() {
  const currentMode = useSelector((state) => state.mode.currentMode);

  return (
    <Tab.Navigator
      screenOptions={({ route }) => ({
        headerShown: false,
        tabBarShowLabel: false,
        tabBarStyle: {
          ...styles.tabBar,
          backgroundColor: currentMode === 'Boy' ? color.BoyColors.Primary : color.GirlColors.Primary,
        },
        tabBarIcon: ({ focused }) => {
          let iconSource;

          if (route.name === 'Home') {
            iconSource = require('@/app/assets/tab/home.png');
          } else if (route.name === 'Story') {
            iconSource = require('@/app/assets/tab/story.png');
          } else if (route.name === 'Settings') {
            iconSource = require('@/app/assets/tab/settings.png');
          }

          return <Image source={iconSource} style={styles.icon} />;
        },
      })}
    >
      <Tab.Screen name="Home" component={Home} />
      <Tab.Screen name="Story" component={Story} />
      <Tab.Screen name="Settings" component={Settings} />
    </Tab.Navigator>
  );
}

function MainStack() {
  return (
    <Stack.Navigator screenOptions={{ headerShown: false }}>
      <Stack.Screen name="SignIn" component={SignIn} />
      <Stack.Screen name="ChildSignUp" component={ChildSignUp} />
      <Stack.Screen name="ResetPassword" component={ResetPassword} />
      <Stack.Screen name="KidsMode" component={KidsMode} />
      <Stack.Screen name="Game" component={Game} />
    </Stack.Navigator>
  );
}

export default function Index() {
  return (
    <Provider store={store}>
      <Stack.Navigator screenOptions={{ headerShown: false }}>
        <Stack.Screen name="Stack" component={MainStack} />
        <Stack.Screen name="Main" component={MainTab} />
      </Stack.Navigator>
    </Provider>
  );
}

const styles = StyleSheet.create({
  icon: {
    width: 40,
    height: 40,
    resizeMode: 'contain',
  },
  tabBar: {

    height: 70,
    borderRadius: 40,
    marginHorizontal: 10,
    elevation: 5,
    justifyContent:'center',
    alignItems:'center',
    marginBottom:40,
    position: 'absolute',
    paddingTop:10,
  },
});
