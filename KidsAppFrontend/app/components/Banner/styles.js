import color from '@/app/styles/color';
import {StyleSheet} from 'react-native'

const styles = StyleSheet.create({
    container: {
        flexDirection: 'row',
        alignItems: 'center',
        padding: 10,
        margin:20,
      },
      imageContainer:{
        backgroundColor:color.GirlColors.Secondary,
        padding:10,
        borderRadius:40

      },
      image: {
        width: 60,
        height: 60,
        marginRight: 10,
      },
      score: {
        fontSize: 16,
        fontWeight: 'bold',
        color: color.GirlColors.Secondary,
        padding:10,
        marginRight:10
      },
      innerContainer:{
        backgroundColor:color.GirlColors.Primary,
        flex:1,
        alignItems:'flex-end',
        borderTopRightRadius:20,
        borderBottomRightRadius:20
      }
});

export default styles