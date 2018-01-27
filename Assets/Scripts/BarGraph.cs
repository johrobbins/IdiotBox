﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarGraph : MonoBehaviour {

    public class Data : Dictionary<Timeslot, Dictionary<Demographic, int>>
    {
        public void AddData(Timeslot t, Demographic d, int viewership)
        {
            Debug.Log(" Adding " + t + " " + d + " " + viewership);
            Dictionary<Demographic, int> value;
            if (!ContainsKey(t))
            {
                Add(t, new Dictionary<Demographic, int>());
            }

            if (!this.TryGetValue(t, out value))
            {
                value.Add(d, viewership);
            }
            else
            {
                this[t][d] = viewership;
            }
        }
    }

    public Material toddlers;
    public Material armchairAthletes;
    public Material closetRomantics;
    public Material adrenalineJunkies;

    public Image graphImageObject;

    private int barFixedWidth = 20;

    private Data data = new Data();
    private Dictionary<String, Image> bars = new Dictionary<String, Image>();

    void Awake()
    {
        Dictionary<Demographic, Material> colors = new Dictionary<Demographic, Material>();
        colors.Add(Demographic.Toddlers, toddlers);
        colors.Add(Demographic.ArmchairAthletes, armchairAthletes);
        colors.Add(Demographic.ClosetRomantics, closetRomantics);
        colors.Add(Demographic.AdrenalineJunkies, adrenalineJunkies);

        foreach (Timeslot t in Enum.GetValues(typeof(Timeslot)))
        {
            GameObject horizontal = new GameObject();
            HorizontalLayoutGroup hlg = horizontal.AddComponent<HorizontalLayoutGroup>();
            hlg.name = t.ToString() + "Group";
            horizontal.GetComponent<RectTransform>().parent = this.transform;
            hlg.childControlHeight = false;
            hlg.childControlWidth = false;
            hlg.childForceExpandHeight = false;
            hlg.childForceExpandWidth = false;
            horizontal.GetComponent<RectTransform>().sizeDelta = new Vector2(200, barFixedWidth);


            foreach (Demographic d in Enum.GetValues(typeof(Demographic)))
            {
                data.AddData(t, d, 0);

                String key = t.ToString() + "-" + d.ToString();
                Image bar = Instantiate(graphImageObject);
                bar.material = colors[d];
                
                bars.Add(key, bar);
                bar.GetComponent<RectTransform>().sizeDelta = new Vector2(data[t][d], barFixedWidth);
                bar.GetComponent<RectTransform>().parent = horizontal.transform;
            }
        }
    }

    void Start()
    {
        data.AddData(Timeslot.Morning, Demographic.Toddlers, 70);
        data.AddData(Timeslot.Morning, Demographic.ArmchairAthletes, 10);

        data.AddData(Timeslot.Daytime, Demographic.ClosetRomantics, 50);
        data.AddData(Timeslot.Daytime, Demographic.ArmchairAthletes, 20);

        data.AddData(Timeslot.PrimeTime, Demographic.Toddlers, 10);
        data.AddData(Timeslot.PrimeTime, Demographic.ClosetRomantics, 80);
        data.AddData(Timeslot.PrimeTime, Demographic.AdrenalineJunkies, 10);

        data.AddData(Timeslot.Night, Demographic.AdrenalineJunkies, 10);
    }

    void Update()
    {
        Array timeslots = Enum.GetValues(typeof(Timeslot));
        Array.Reverse(timeslots);
        foreach (Timeslot t in timeslots)
        {
            int totalAudienceOffset = 0;
            foreach (Demographic d in Enum.GetValues(typeof(Demographic)))
            {
                String key = t.ToString() + "-" + d.ToString();
                RectTransform rect = bars[key].GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(data[t][d], barFixedWidth);
                totalAudienceOffset += data[t][d];
            }
        }
    }
}