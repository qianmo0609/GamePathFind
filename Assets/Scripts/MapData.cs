using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData
{
    /*    
       / \
	    |   
	    |  
	    z   
	    |   
	    --------- x --------->
     */
    //第一维是纵向，第二维是横向
    public static int[,] m_MapData = new int[12, 11] {
        { 0,0,0,0,0,0,0,0,0,0,0 },
        { 0,0,0,0,0,0,0,1,1,0,0 },
        { 0,0,1,1,0,0,0,0,1,0,0 },
        { 0,0,0,1,0,0,0,0,0,0,0 },
        { 0,0,0,0,0,0,0,0,0,0,0 },
        { 0,0,0,0,0,0,0,0,0,0,0 },
        { 0,0,0,0,0,0,0,0,0,0,0 },
        { 0,0,0,0,0,0,1,0,0,0,0 },
        { 0,0,0,0,0,0,1,0,0,0,0 },
        { 0,0,0,0,0,0,0,1,0,0,0 },
        { 0,0,0,0,1,1,1,1,1,0,0 },
        { 0,0,0,0,1,1,1,1,1,0,0 }
    };
}
